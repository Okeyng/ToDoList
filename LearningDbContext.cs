using Microsoft.EntityFrameworkCore;
using ToDoList.Configuration;
using ToDoList.Models;

namespace ToDoList
{
    public class LearningDbContext(DbContextOptions<LearningDbContext> options)
        :DbContext(options)
    {
        public DbSet<NoteEntity> Notes {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
        }
    }
}