// Este archivo define el modelo de entidad para una tarea en la base de datos.
// El modelo de entidad se utiliza para mapear las propiedades de la tarea a una tabla de base de datos
// y para definir la relación entre la tarea y la lista a la que pertenece. La entidad incluye identificadores,
// títulos, estado de finalización y el identificador de la lista asociada.

using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Task
    {
        public int TaskId { get; set; } // Identificador único de la tarea.

        [Required]
        public string Title { get; set; } // Título de la tarea.

        public bool IsComplete { get; set; } // Indica si la tarea está completada.

        public int ListId { get; set; } // Identificador de la lista a la que pertenece la tarea.
        public List List { get; set; } // Referencia a la lista a la que pertenece la tarea.
    }
}
