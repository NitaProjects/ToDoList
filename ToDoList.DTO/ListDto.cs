using System.Collections.Generic;

namespace ToDoList.DTO
{
    public class ListDto
    {
        // Identificador único de la lista
        public int ListId { get; set; }

        // Nombre de la lista
        public string Name { get; set; }

        // Sublistas de esta lista, permitiendo null o listas vacías
        public ICollection<ListDto> SubLists { get; set; } = new List<ListDto>();

        // Tareas de esta lista, permitiendo null o listas vacías
        public ICollection<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
