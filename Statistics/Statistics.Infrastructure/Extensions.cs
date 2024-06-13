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
using Statistics.Application;
using Statistics.Application.Events;
using Statistics.Application.Services;
using Statistics.Core.Repositories;
using Statistics.Infrastructure.Exceptions;
using Statistics.Infrastructure.Mongo.Documents;
using Statistics.Infrastructure.Mongo.Repositories;
using Statistics.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IStatisticEventRepository, StatisticEventMongoRepository>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddMongoRepository<StatisticAmountChangeDocument, Guid>("statistic_events")
                .AddMongoRepository<HotelDocument, Guid>("hotels")
                .AddRabbitMq();
        }

        public static IApplicationBuilder UserInfrastructure(this IApplicationBuilder app) 
        {
            app.UseErrorHandler()
                .UseConvey()
                .UsePublicContracts<ContractAttribute>() // możliwe że to wymaga aby swagger inaczej wpiąć.
                .UseRabbitMq() //rzeczy do rabbita na końcu
                .SubscribeEvent<ReservationPurchasePending>()
                .SubscribeEvent<ReservationPurchased>()
                ;
            
            return app;
        }

    }
}
