using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.WebApi;
using Microsoft.AspNetCore.Mvc;
using Tours.Application;
using Tours.Application.Queries;
using Tours.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConvey()
    .AddWebApi()
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UserInfrastructure();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();