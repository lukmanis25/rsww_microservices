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
using Purchases.Application;
using Purchases.Application.Events;
using Purchases.Application.Events.Externals;
using Purchases.Application.Services;
using Purchases.Core.Purchases;
using Purchases.Infrastructure.Exceptions;
using Purchases.Infrastructure.Mongo.Documents;
using Purchases.Infrastructure.Mongo.Repositories;
using Purchases.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchases.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IPurchaseRepository, PurchaseMongoRepository>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddMongoRepository<PurchaseDocument, Guid>("purchases")
                .AddRabbitMq();
        }

        public static IApplicationBuilder UserInfrastructure(this IApplicationBuilder app) 
        {
            app.UseErrorHandler()
                .UseConvey()
                .UsePublicContracts<ContractAttribute>() // możliwe że to wymaga aby swagger inaczej wpiąć.
                .UseRabbitMq() //rzeczy do rabbita na końcu
                .SubscribeEvent<ReservationPurchasePending>()
                .SubscribeEvent<ReservationCancelled>()
                ;
            
            return app;
        }

    }
}
