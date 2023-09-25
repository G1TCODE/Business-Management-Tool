using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using BMT.Domain.Users;
using BMT.Infrastructure.Repositories;
using BMT.Domain.Shopping_Cart;
using BMT.Domain.Locations;
using BMT.Domain.Products;
using BMT.Domain.Orders;
using BMT.Domain.Abstractions;
using BMT.Application.Abstractions.DataLink;
using BMT.Infrastructure.Data;

namespace BMT.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection infrastructureServices, 
            IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("<<<YOUR DB HERE>>>") ??
                throw new ArgumentNullException(nameof(configuration));

            infrastructureServices.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            infrastructureServices.AddScoped<IOrderRepository, OrderRepository>();

            infrastructureServices.AddScoped<IManagerRepository, ManagerRepository>();

            infrastructureServices.AddScoped<IProductRepository, ProductRepository>();

            infrastructureServices.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            infrastructureServices.AddScoped<IStoreRepository, StoreRepository>();

            infrastructureServices.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

            infrastructureServices.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

            return infrastructureServices;
        }
    }
}
