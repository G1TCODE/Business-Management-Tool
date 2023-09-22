using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Orders.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BMT.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection applicationServices)
        {
            applicationServices.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            applicationServices.AddTransient<OrderPricingService>();

            return applicationServices;
        }
    }
}
