using Convey;
using Reservations.Infrastructure;
using Reservations.Application;
using Convey.WebApi;
using Reservations.Application.Commands;
using Convey.CQRS.Commands;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConvey()
    .AddWebApi()
    .AddApplication()
    .AddInfrastructure();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UserInfrastructure();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/api/reservation", async (ICommandDispatcher commandDispatcher, AddOfferReservation command) =>
{
    await commandDispatcher.SendAsync(command);
    return "resource created";
})
.WithName("PostReservations")
.WithOpenApi();

app.Run();
