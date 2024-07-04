namespace ToDoList.DTO
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public int ListId { get; set; }
    }
}
