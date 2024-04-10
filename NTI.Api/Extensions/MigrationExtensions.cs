using Microsoft.EntityFrameworkCore;
using NTI.Infrastructure.Context;

namespace NTI.Api.Extensions
{
    /// <summary>
    /// Extension methods for the Program class
    /// </summary>
    public static class MigrationExtensions
    {
        /// <summary>
        /// Applies the migrations to the database
        /// </summary>
        /// <param name="app"></param>
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
            context.Database.Migrate();
        }
    }
}