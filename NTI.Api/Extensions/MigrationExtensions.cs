using Microsoft.EntityFrameworkCore;
using NTI.Infrastructure.Context;

namespace NTI.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
            context.Database.Migrate();
        }
    }
}