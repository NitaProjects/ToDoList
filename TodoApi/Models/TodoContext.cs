using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        // DbSet existente para TodoItems
        public DbSet<TodoItem> TodoItems { get; set; } = null!;

        // Nuevos DbSet para Listas y Tareas
        public DbSet<List> Lists { get; set; } = null!;
        public DbSet<Task> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación autorreferenciada para List
            modelBuilder.Entity<List>()
                .HasMany(l => l.SubLists)
                .WithOne(l => l.ParentList)
                .HasForeignKey(l => l.ParentListId);

            modelBuilder.Entity<List>()
                .HasMany(l => l.Tasks)
                .WithOne(t => t.List)
                .HasForeignKey(t => t.ListId);
        }
    }
}
