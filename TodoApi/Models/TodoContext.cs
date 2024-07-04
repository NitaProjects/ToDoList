// Este archivo define el contexto de la base de datos para la aplicación de lista de tareas.
// El contexto de la base de datos se utiliza para configurar y gestionar las entidades y sus relaciones
// en la base de datos utilizando Entity Framework Core. Este archivo define los DbSet para las listas y tareas,
// y configura las relaciones entre las entidades en el método OnModelCreating.

using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { } // Constructor para configurar el contexto con opciones específicas.

        public DbSet<List> Lists { get; set; } // DbSet para la entidad List.
        public DbSet<Task> Tasks { get; set; } // DbSet para la entidad Task.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura la relación uno a muchos entre List y SubLists
            modelBuilder.Entity<List>()
                .HasMany(l => l.SubLists) // Una lista tiene muchas sublistas.
                .WithOne(l => l.ParentList) // Cada sublista tiene una lista principal.
                .HasForeignKey(l => l.ParentListId); // La clave foránea en SubLists es ParentListId.

            // Configura la relación uno a muchos entre List y Tasks
            modelBuilder.Entity<List>()
                .HasMany(l => l.Tasks) // Una lista tiene muchas tareas.
                .WithOne(t => t.List) // Cada tarea tiene una lista.
                .HasForeignKey(t => t.ListId); // La clave foránea en Tasks es ListId.
        }
    }
}
