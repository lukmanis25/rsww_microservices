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

var app = builder.Build();

//app.UseLogging();

app.UserInfrastructure();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/reservations", async (ICommandDispatcher commandDispatcher, AddReservationWithoutId command) =>
{
    Guid reservationId = Guid.NewGuid();
    await commandDispatcher.SendAsync(new AddReservation(reservationId, command));
    return Results.Created($"/api/reservation/{reservationId}", null);
})
.WithName("PostReservation")
.WithOpenApi();

app.MapGet("/api/reservations/{reservationId}", async (IQueryDispatcher queryDispatcher, [FromRoute] Guid reservationId) =>
{
    var result = await queryDispatcher.QueryAsync(new GetReservation { ReservationId = reservationId });
    return result;
})
.WithName("GetReservation")
.WithOpenApi();

app.Run();
