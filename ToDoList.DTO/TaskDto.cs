namespace ToDoList.DTO
{
    public class TaskDto
    {
        // Identificador único de la tarea
        public int TaskId { get; set; }

        // Título de la tarea
        public string Title { get; set; }

        // Indicador de si la tarea está completada
        public bool IsComplete { get; set; }

        // Identificador de la lista a la que pertenece esta tarea
        public int ListId { get; set; }
    }
}
