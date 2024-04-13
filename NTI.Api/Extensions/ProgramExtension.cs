
using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NTI.Application.AppContext;
using NTI.Application.InputModels.Customers;
using NTI.Application.InputModels.Items;
using NTI.Application.InputModels.Login;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;
using NTI.Application.Mappings.Items;
using NTI.Application.Options;
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
            services.AddScoped<ICustomerItemsRepository, CustomerItemsRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
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
            services.AddScoped<ICustomerItemService, CustomerItemsService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ApplicationContext, ApplicationContext>();
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
            services.AddScoped<IValidator<LoginInputRecord>, LoginInputValidator>();
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

                config.AddSecurityDefinition("Bearer", new()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT token",
                });

                config.AddSecurityRequirement(new()
                {
                   {
                       new OpenApiSecurityScheme()
                       {
                           Reference = new()
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer",
                           }
                       },
                       Array.Empty<string>()
                   }
                });


            });

            return services;
        }

        internal static IServiceCollection ConfigureAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            return services;
        }

        internal static void ConfigureCors(this IServiceCollection services)
        {

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        }

        internal static IServiceCollection ConfigureOptions(this IServiceCollection services, WebApplicationBuilder builder)
        {
            return services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
        }
    }
}