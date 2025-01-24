using To_Do_List.Dto.TaskDto;

namespace To_Do_List.Interface
{
    public interface ITaskRepositroy
    {
        public Task<TaskViewDto> GetTaskDto(int taskId,string username);
        public Task<Models.Task?> GetTask(int TaskId,string username);

        public Task<List<AllTasksViewDto>> AllTasksView(string username);

        public Task<int?> ToCreateTask(Models.Task task,string username);

        public Task<int> ToUpdateTask(Models.Task task , UpdateTaskDto updateTask);

        public Task<int> ToDeleteTask(int TaskId);

    }
}
