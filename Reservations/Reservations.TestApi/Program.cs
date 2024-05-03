using Convey;
using Reservations.Infrastructure;
using Reservations.Application;
using Convey.WebApi;
using Reservations.Application.Commands;
using Convey.CQRS.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Convey.WebApi.CQRS;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConvey()
    .AddWebApi()
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UserInfrastructure();

//MOZNA TAK USTAWIAC ENDPOINT ALE SWAGGER WTEDY NIE DZIALA
//app.UseDispatcherEndpoints(endpoints => endpoints
//        .Post<AddOfferReservation>("/api/reservation",
//            afterDispatch: (cmd, ctx) => ctx.Response.Created($"/api/reservation/{cmd.ReservationId}"))
//        );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//dla query bêdzie IQueryDispatcher
app.MapPost("/api/reservation", async (ICommandDispatcher commandDispatcher, AddOfferReservation command) =>
{
    await commandDispatcher.SendAsync(command);
    return Results.Created($"/api/reservation/{command.ReservationId}", null);
})
.WithName("PostReservation")
.WithOpenApi();

app.Run();
