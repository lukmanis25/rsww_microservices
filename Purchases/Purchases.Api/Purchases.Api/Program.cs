using Convey;
using Purchases.Infrastructure;
using Purchases.Application;
using Convey.WebApi;
using Convey.CQRS.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Convey.WebApi.CQRS;
using Convey.CQRS.Queries;
using Purchases.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Purchases.Application.DTO;
using Purchases.Application.Commands;

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

app.MapPost("/api/purchases", async (ICommandDispatcher commandDispatcher, MakePayment command) =>
{
    await commandDispatcher.SendAsync(command);
    return Results.Accepted();
})
.WithName("MakePurchase")
.WithOpenApi();

app.MapGet("/api/purchases/{reservationId}", async (IQueryDispatcher queryDispatcher, [FromRoute] Guid reservationId) =>
{
    var result = await queryDispatcher.QueryAsync(new GetPurchaseByReservation { ReservationId = reservationId });
    return result;
})
.WithName("GetPurchesByReservationId")
.WithOpenApi();

app.Run();
