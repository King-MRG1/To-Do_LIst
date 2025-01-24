using System.ComponentModel.DataAnnotations.Schema;

namespace To_Do_List.Models
{
    public class Task
    {
        public int taskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; } = false;

        [ForeignKey("User")]
        public int userId { get; set; }
        public User User { get; set; }
    }
}
