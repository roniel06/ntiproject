using Microsoft.EntityFrameworkCore;
using NTI.Infrastructure.Context;

namespace NTI.Test.Helpers
{
    public static class TestDbContextFactory
    {

        public static ProjectDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ProjectDbContext>()
               .UseSqlite("DataSource=:memory:").Options;

            var context = new ProjectDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        public static void Destroy(ProjectDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

     
    }
}