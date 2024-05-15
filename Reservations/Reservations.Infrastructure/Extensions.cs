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
using Reservations.Application;
using Reservations.Application.Events.External;
using Reservations.Application.Services;
using Reservations.Core.Repositories;
using Reservations.Infrastructure.Exceptions;
using Reservations.Infrastructure.Mongo.Documents;
using Reservations.Infrastructure.Mongo.Repositories;
using Reservations.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IReservationRepository, ReservationMongoRepository>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddMongoRepository<ReservationDocument, Guid>("reservations")
                .AddRabbitMq();
        }

        public static IApplicationBuilder UserInfrastructure(this IApplicationBuilder app) 
        {
            app.UseErrorHandler()
                .UseConvey()
                .UsePublicContracts<ContractAttribute>() // możliwe że to wymaga aby swagger inaczej wpiąć
                .UseRabbitMq() //rzeczy do rabbita na końcu
                .SubscribeEvent<PurchaseCompleted>()
                .SubscribeEvent<PurchaseCancelled>()
                .SubscribeEvent<TransportReserved>()
                ;

            return app;
        }

    }
}
