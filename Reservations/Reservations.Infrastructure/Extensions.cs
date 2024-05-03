using Convey;
using Microsoft.AspNetCore.Builder;
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
            return builder;
        }

        public static IApplicationBuilder UserInfrastructure(this IApplicationBuilder app) 
        {
            app.UseConvey();
            
            return app;
        }

    }
}
