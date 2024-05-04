using Convey;
using Convey.CQRS.Queries;
using Convey.Docs.Swagger;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Core.Repositories;
using Reservations.Infrastructure.Exceptions;
using Reservations.Infrastructure.Mongo.Documents;
using Reservations.Infrastructure.Mongo.Repositories;
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
            builder.Services.AddTransient<IOfferReservationRepository, OfferReservationMongoRepository>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddMongo()
                .AddMongoRepository<OfferReservationDocument, Guid>("reservations");
                //.AddWebApiSwaggerDocs();
        }

        public static IApplicationBuilder UserInfrastructure(this IApplicationBuilder app) 
        {
            app.UseErrorHandler()
                //.UseSwaggerDocs()
                .UseConvey();
            
            return app;
        }

    }
}
