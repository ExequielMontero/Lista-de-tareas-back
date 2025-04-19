using Api_LIsta_de_tareas.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_LIsta_de_tareas.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd(); // Configuración correcta para autoincremento
        }
    }
}
