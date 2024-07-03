using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Task
    {
        // Identificador único de la tarea
        public int TaskId { get; set; }

        // Título de la tarea
        [Required]
        public string Title { get; set; }

        // Indicador de si la tarea está completada
        public bool IsComplete { get; set; }

        // Llave foránea para la lista a la que pertenece
        public int ListId { get; set; }
        public List List { get; set; }
    }
}
