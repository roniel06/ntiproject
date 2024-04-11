
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NTI.Application.InputModels.Customers;
using NTI.Application.InputModels.Items;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;
using NTI.Application.Mappings.Items;
using NTI.Application.Services;
using NTI.Application.Validators;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories;

namespace NTI.Api.Extensions
{
    /// <summary>
    /// Extension methods for the Program class
    /// </summary>
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
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            return services;
        }

        /// <summary>
        /// Injects the services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IItemsService, ItemsService>();
            services.AddScoped<ICustomersService, CustomersService>();
            return services;
        }

        /// <summary>
        /// Configures the DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public static void ConfigureDbContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ProjectDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
            });
        }

        /// <summary>
        /// Sets up the AutoMapper
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ItemMapping));
            return services;
        }


        /// <summary>
        /// Configures the FluentValidations
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureFluentValidations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddScoped<IValidator<ItemInputModel>, ItemsValidator>();
            services.AddScoped<IValidator<CustomerInputModel>, CustomersValidator>();
            return services;
        }

        /// <summary>
        /// Configures the Swagger documentation
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new()
                {
                    Version = "v1",
                    Title = "NTI.Api",
                    Description = "Items and Customer Api"
                });
                config.UseInlineDefinitionsForEnums();

                // Include XML comments from the controllers' XML documentation files
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);


                //config.AddSecurityDefinition("Bearer", new()
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Description = "JWT token",
                //});

                //config.AddSecurityRequirement(new()
                //{

                //    {
                //        new OpenApiSecurityScheme()
                //        {
                //            Reference = new()
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer",
                //            }
                //        },
                //        Array.Empty<string>()
                //    }

                //});


            });

            return services;
        }

        internal static void ConfigureCors(this IServiceCollection services)
        {

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        }
    }
}