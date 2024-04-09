
using Microsoft.EntityFrameworkCore;
using NTI.Application.Interfaces.Repositories;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories;

namespace NTI.Api.Extensions
{
    public static class ProgramExtension
    {
        /// <summary>
        /// Injects the repositories into the services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemsRepository>();
            return services;
        }

        // public static IServiceCollection ConfigureServices(this IServiceCollection services)
        // {
        //     services.AddScoped<IItemService, ItemService>();
        //     return services;
        // }


        public static void ConfigureDbContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ProjectDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
            });
        }


        
    }
}