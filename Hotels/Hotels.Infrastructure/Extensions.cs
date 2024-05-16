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
using Hotels.Application;
using Hotels.Application.Events;
using Hotels.Application.Services;
using Hotels.Core.Repositories;
using Hotels.Infrastructure.Exceptions;
using Hotels.Infrastructure.Mongo.Documents;
using Hotels.Infrastructure.Mongo.Repositories;
using Hotels.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IHotelEventRepository, HotelEventMongoRepository>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddMongoRepository<HotelRoomAmountChangeDocument, Guid>("hotel_events")
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
