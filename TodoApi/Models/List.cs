using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class List
    {
        public int ListId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<List> SubLists { get; set; } = new List<List>();

        public ICollection<Task> Tasks { get; set; } = new List<Task>();

        public int? ParentListId { get; set; }
        public List ParentList { get; set; }
    }
}
