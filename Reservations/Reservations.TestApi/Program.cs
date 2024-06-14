using Convey;
using Reservations.Infrastructure;
using Reservations.Application;
using Convey.WebApi;
using Reservations.Application.Commands;
using Convey.CQRS.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Convey.WebApi.CQRS;
using Convey.CQRS.Queries;
using Reservations.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.DTO;
using Convey.Logging;
using Convey.Types;
using Microsoft.AspNetCore;
using Reservations.Application.SignalRHub;

//namespace Reservations.Api
//{
//    public class Program
//    {
//        public static async Task Main(string[] args)
//            => await CreateWebHostBuilder(args)
//                .Build()
//                .RunAsync();

//        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
//            => WebHost.CreateDefaultBuilder(args)
//                .ConfigureServices(services => services
//                    .AddConvey()
//                    .AddWebApi()
//                    .AddApplication()
//                    .AddInfrastructure()
//                    .Build())
//                .Configure(app => app
//                .UserInfrastructure()
//                    .UseDispatcherEndpoints(endpoints => endpoints
//                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
//                        .Post<AddReservation>("resources",
//                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"resources/{cmd.Id}"))
//                ))
//            //.UseLogging()
//            ;
//    }
//}

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConvey()
    .AddWebApi()
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

//app.UseLogging();

app.UserInfrastructure();

app.MapHub<ReservationHub>("reservation-hub");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/reservations", async (ICommandDispatcher commandDispatcher, AddReservationWithoutId command) =>
{
    Guid reservationId = Guid.NewGuid();
    await commandDispatcher.SendAsync(new AddReservation(reservationId, command));
    //return Results.Created($"/api/reservation/{reservationId}", null);
    return new { ReservationId=reservationId };
})
.WithName("AddReservation")
.WithOpenApi();

app.MapGet("/api/reservations/{reservationId}", async (IQueryDispatcher queryDispatcher, [FromRoute] Guid reservationId) =>
{
    var result = await queryDispatcher.QueryAsync(new GetReservation { ReservationId = reservationId });
    return result;
})
.WithName("GetReservation")
.WithOpenApi();

app.MapPost("/api/reservations/{reservationId}", async (ICommandDispatcher commandDispatcher, [FromRoute] Guid reservationId) =>
{
    await commandDispatcher.SendAsync(new CancelReservation { ReservationId = reservationId});
    return Results.Accepted();
})
.WithName("CancelReservation")
.WithOpenApi();

app.MapGet("/api/users/{customerId}/reservations", async (IQueryDispatcher queryDispatcher, [FromRoute] Guid customerId) =>
{
    var result = await queryDispatcher.QueryAsync(new GetUserReservations { CustomerId = customerId });
    return result;
})
.WithName("GetUserReservations")
.WithOpenApi();

app.Run();
