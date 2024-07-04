// Este archivo define el Data Transfer Object (DTO) para una lista de tareas.
// El DTO se utiliza para transferir datos entre el cliente y el servidor en una forma
// que es adecuada para la comunicación a través de la API. El DTO para la lista de tareas
// incluye identificadores, nombres, sublistas y tareas asociadas.

using System.Collections.Generic;

namespace ToDoList.DTO
{
    public class ListDto
    {
        public int ListId { get; set; } // Identificador único de la lista.
        public string? Name { get; set; } // Nombre de la lista.
        public ICollection<ListDto> SubLists { get; set; } = new List<ListDto>(); // Colección de sublistas anidadas.
        public ICollection<TaskDto> Tasks { get; set; } = new List<TaskDto>(); // Colección de tareas asociadas a la lista.
    }
}
