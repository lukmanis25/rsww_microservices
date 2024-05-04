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

//MOZNA TAK USTAWIAC ENDPOINT ALE SWAGGER WTEDY GORZEJ DZIA£¥ I TRZEBA DODAC DO W INFRA
//app.UseDispatcherEndpoints(endpoints => endpoints
//    .Get<GetOfferReservation, OfferReservationDto>("reservations/{offerId}")
//    .Post<AddOfferReservation>("reservations",
//        afterDispatch: (cmd, ctx) => ctx.Response.Created($"reservations/{cmd.OfferId}"))
//    );


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/reservation", async (ICommandDispatcher commandDispatcher, AddOfferReservation command) =>
{
    await commandDispatcher.SendAsync(command);
    return Results.Created($"/api/reservation/{command.OfferId}", null);
})
.WithName("PostReservation")
.WithOpenApi();

app.MapGet("/api/reservation/{offerId}", async (IQueryDispatcher queryDispatcher, [FromRoute] Guid offerId) =>
{
    var result = await queryDispatcher.QueryAsync(new GetOfferReservation { OffertId = offerId });
    return result;
})
.WithName("GetReservation")
.WithOpenApi();

app.Run();
