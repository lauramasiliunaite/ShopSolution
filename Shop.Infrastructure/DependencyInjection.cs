using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Services;

namespace Shop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlServer(cfg.GetConnectionString("DefaultConnection")));

            services.AddScoped<ProductRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<ProductSqlRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
