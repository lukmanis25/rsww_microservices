using Convey;
using Convey.CQRS.Queries;
using Convey.Docs.Swagger;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Transports.Application;
using Transports.Application.Events;
using Transports.Application.Services;
using Transports.Core.Repositories;
using Transports.Infrastructure.Exceptions;
using Transports.Infrastructure.Mongo.Documents;
using Transports.Infrastructure.Mongo.Repositories;
using Transports.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transports.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<ITransportEventRepository, TransportEventMongoRepository>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddMongoRepository<TransportAmountChangeDocument, Guid>("transport_events")
                .AddRabbitMq();
        }

        public static IApplicationBuilder UserInfrastructure(this IApplicationBuilder app) 
        {
            app.UseErrorHandler()
                .UseConvey()
                .UsePublicContracts<ContractAttribute>() // możliwe że to wymaga aby swagger inaczej wpiąć.
                .UseRabbitMq() //rzeczy do rabbita na końcu
                .SubscribeEvent<ReservationPendingCreated>()
                .SubscribeEvent<ReservationCancelled>()
                ;
            
            return app;
        }

    }
}
