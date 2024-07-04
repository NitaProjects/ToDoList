using System.Collections.Generic;

namespace ToDoList.DTO
{
    public class ListDto
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public ICollection<ListDto> SubLists { get; set; } = new List<ListDto>();
        public ICollection<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
