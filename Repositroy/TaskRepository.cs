using Microsoft.EntityFrameworkCore;
using To_Do_List.Dto;
using To_Do_List.Dto.TaskDto;
using To_Do_List.Interface;
using To_Do_List.Models;

namespace To_Do_List.Repositroy
{
    public class TaskRepository : ITaskRepositroy
    {
        private readonly ListContext _context;
        private readonly IUserRepositroy _userRepositroy;
        public TaskRepository(ListContext context, IUserRepositroy userRepositroy)
        {
            _context = context;
            _userRepositroy = userRepositroy;
        }

        public async Task<List<AllTasksViewDto>> AllTasksView(string username)
        {
            return await _context.Tasks.Include(t=>t.User).Where(t=>t.User.Username == username).Select(t => t.AllTasksViewDto()).ToListAsync();
        }

        public async Task<Models.Task?> GetTask(int TaskId,string username)
        {
            return await _context.Tasks.Include(t => t.User).FirstOrDefaultAsync(t => t.taskId == TaskId && t.User.Username == username);
        }

        public async Task<TaskViewDto?> GetTaskDto(int taskid,string usrname)
        {
            return await _context.Tasks.Include(t => t.User).Where(t => t.taskId == taskid && t.User.Username == usrname).Select(t => t.TaskViewDto()).FirstOrDefaultAsync();
        }

        public async Task<int?> ToCreateTask(Models.Task task,string username)
        {
            var user = await _userRepositroy.GetByUsername(username);

            if (user == null)
                return 0;

            task.userId = user.UserId;
            task.User = user;

            await _context.Tasks.AddAsync(task);

            return await _context.SaveChangesAsync();

        }

        public async Task<int> ToDeleteTask(int TaskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.taskId == TaskId);

            if(task == null) return 0;

             _context.Tasks.Remove(task);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> ToUpdateTask(Models.Task task, UpdateTaskDto updateTask)
        {
            task.Name = updateTask.Title;
            task.Description = updateTask.Description;
            task.IsComplete = updateTask.IsCompleted;

            return await _context.SaveChangesAsync();
        }
    }
}
