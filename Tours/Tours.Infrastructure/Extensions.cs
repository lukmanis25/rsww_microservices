using System;
using Convey;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tours.Application.Services;
using Tours.Application.Events;
using Tours.Infrastructure.Exceptions;
using Tours.Infrastructure.Mongo.Documents;
using Tours.Infrastructure.Mongo.Repositories;
using Tours.Infrastructure.Services;

namespace Tours.Infrastructure;

public static class Extensions
{
    public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
    {
        builder.Services.AddTransient<ITourRepository, TourMongoRepository>();
        builder.Services.AddTransient<IMessageBroker, MessageBroker>();

        return builder.AddQueryHandlers()
            .AddInMemoryQueryDispatcher()
            .AddErrorHandler<ExceptionToResponseMapper>()
            .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
            .AddMongo()
            .AddMongoRepository<TourDocument, Guid>("tours")
            .AddRabbitMq();
    }

    public static IApplicationBuilder UserInfrastructure(this IApplicationBuilder builder)
    {
        builder.UseErrorHandler()
            .UseConvey()
            .UseRabbitMq()
            .SubscribeEvent<HotelAvailabilityChanged>()
            .SubscribeEvent<TransportAvailabilityChanged>();

        return builder;
    }
}