using Microsoft.EntityFrameworkCore;
using ToDoList;

namespace ToDoList.Migrations
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using LearningDbContext dbContext = scope.ServiceProvider.GetRequiredService<LearningDbContext>();
        }
    }
}
