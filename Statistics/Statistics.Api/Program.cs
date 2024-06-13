using Convey;
using Statistics.Infrastructure;
using Statistics.Application;
using Convey.WebApi;
using Convey.CQRS.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Convey.WebApi.CQRS;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Statistics.Application.Queries;
using Statistics.Core.ValueObjects;
using Statistics.Core;


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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/destination_statistic/{statisticType}", async (IQueryDispatcher queryDispatcher, [FromRoute]  StatisticType statisticType) =>
{
    var result = await queryDispatcher.QueryAsync(new GetDestinationStatistic { StatisticType = statisticType });
    return result;
})
.WithName("GetDestionationStatistic")
.WithOpenApi();

app.MapGet("/api/hotel_statistic/{statisticType}", async (IQueryDispatcher queryDispatcher, [FromRoute] StatisticType statisticType) =>
{
    var result = await queryDispatcher.QueryAsync(new GetHotelStatistic { StatisticType = statisticType });
    return result;
})
.WithName("GetHotelStatistic")
.WithOpenApi();

app.MapGet("/api/room_statistic/{statisticType}", async (IQueryDispatcher queryDispatcher, [FromRoute] StatisticType statisticType) =>
{
    var result = await queryDispatcher.QueryAsync(new GetRoomStatistic { StatisticType = statisticType });
    return result;
})
.WithName("GetRoomStatistic")
.WithOpenApi();

app.Run();
