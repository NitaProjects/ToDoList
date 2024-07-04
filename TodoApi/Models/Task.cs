using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsComplete { get; set; }

        public int ListId { get; set; }
        public List List { get; set; }
    }
}
