using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class List
    {
        // Identificador único de la lista
        public int ListId { get; set; }

        // Nombre de la lista, campo requerido
        [Required]
        public string Name { get; set; }

        // Propiedad de navegación para sublistas
        public ICollection<List> SubLists { get; set; } = new List<List>();

        // Propiedad de navegación para tareas
        public ICollection<Task> Tasks { get; set; } = new List<Task>();

        // Llave foránea para la lista padre (si es una sublista)
        public int? ParentListId { get; set; }
        public List ParentList { get; set; }
    }
}
