// Este archivo define el Data Transfer Object (DTO) para una tarea.
// El DTO se utiliza para transferir datos entre el cliente y el servidor en una forma
// que es adecuada para la comunicación a través de la API. El DTO para la tarea
// incluye identificadores, títulos, estado de finalización y el identificador de la lista asociada.

namespace ToDoList.DTO
{
    public class TaskDto
    {
        public int TaskId { get; set; } // Identificador único de la tarea.
        public string? Title { get; set; } // Título de la tarea.
        public bool IsComplete { get; set; } // Indica si la tarea está completada.
        public int ListId { get; set; } // Identificador de la lista a la que pertenece la tarea.
    }
}
