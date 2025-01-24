using To_Do_List.Dto.TaskDto;

namespace To_Do_List.Dto
{
    public static class TaskMapping
    {
        public static TaskViewDto TaskViewDto(this Models.Task task)
        {
            return new TaskViewDto
            {
                Title = task.Name,
                Description = task.Description,
                IsComplete = task.IsComplete
            };
        }

        public static Models.Task ToCreateTask(this CreateTaskDto taskCreateDto)
        {
            return new Models.Task
            {
                Name = taskCreateDto.Title,
                Description = taskCreateDto.Description,
                IsComplete = false,
            };
        }

        public static AllTasksViewDto AllTasksViewDto(this Models.Task task)
        {
            return new AllTasksViewDto
            {
                Title = task.Name,
                IsComplete = task.IsComplete,
            };
        }   

    }
}
