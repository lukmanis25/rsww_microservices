using Convey;
using Hotels.Infrastructure;
using Hotels.Application;
using Convey.WebApi;
using Convey.CQRS.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Convey.WebApi.CQRS;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;


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

app.Run();
