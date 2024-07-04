// Este archivo define el modelo de entidad para una lista de tareas en la base de datos.
// El modelo de entidad se utiliza para mapear las propiedades de la lista de tareas a una tabla de base de datos
// y para definir las relaciones entre las listas, sublistas y tareas. La entidad incluye identificadores, nombres,
// sublistas, tareas y la referencia a la lista principal (si existe).

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class List
    {
        public int ListId { get; set; } // Identificador único de la lista.

        [Required]
        public string Name { get; set; } // Nombre de la lista.

        public ICollection<List> SubLists { get; set; } = new List<List>(); // Colección de sublistas anidadas.

        public ICollection<Task> Tasks { get; set; } = new List<Task>(); // Colección de tareas asociadas a la lista.

        public int? ParentListId { get; set; } // Identificador de la lista principal si esta lista es una sublista.
        public List ParentList { get; set; } // Referencia a la lista principal si esta lista es una sublista.
    }
}
