
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NTI.Application.InputModels.Items;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;
using NTI.Application.Mappings.Items;
using NTI.Application.Services;
using NTI.Application.Validators;
using NTI.Domain.Models;
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

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IItemsService, ItemsService>();
            return services;
        }


        public static void ConfigureDbContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ProjectDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
            });
        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ItemMapping));

            return services;
        }


        public static IServiceCollection ConfigureFluentValidations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddScoped<IValidator<ItemInputModel>, ItemsValidator>();
            return services;
        }
    }
}