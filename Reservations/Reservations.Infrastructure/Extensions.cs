using Convey;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Core.Repositories;
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
                .AddMongo()
                .AddMongoRepository<OfferReservationDocument, Guid>("resources");
        }

        public static IApplicationBuilder UserInfrastructure(this IApplicationBuilder app) 
        {
            app.UseConvey();
            
            return app;
        }

    }
}
