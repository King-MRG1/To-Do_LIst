using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Dto.TaskDto
{
    public class UpdateTaskDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsCompleted { get; set; } = false;
    }
}
