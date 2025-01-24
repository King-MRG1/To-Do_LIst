using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Auth;
using To_Do_List.Dto;
using To_Do_List.Dto.TaskDto;
using To_Do_List.Interface;

namespace To_Do_List.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepositroy taskRepositroy;
        public TaskController(ITaskRepositroy taskRepositroy)
        {
            this.taskRepositroy = taskRepositroy;
        }

        [BasicAuthorization]
        [HttpGet("all")]
        public async Task<IActionResult> AllTasksView()
        {
            var username = User.Identity.Name;

            var tasks = await taskRepositroy.AllTasksView(username);

            return Ok(tasks);
        }

        [BasicAuthorization]
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTask(int taskId)
        {
            var username = User.Identity.Name;
            var task = await taskRepositroy.GetTaskDto(taskId, username);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [BasicAuthorization]
        [HttpPost()]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto taskCreateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var username = User.Identity.Name;

            var task = taskCreateDto.ToCreateTask();

            await taskRepositroy.ToCreateTask(task, username);

            return Ok("Task Created");
        }

        [BasicAuthorization]
        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] UpdateTaskDto updateTask)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var username = User.Identity.Name;

            var task = await taskRepositroy.GetTask(taskId, username);

            if (task == null)
                return NotFound();

            await taskRepositroy.ToUpdateTask(task, updateTask);

            return Ok("Task Updated");
        }

        [BasicAuthorization]
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var username = User.Identity.Name;

            var task = await taskRepositroy.GetTask(taskId, username);

            if (task == null)
                return NotFound();

            await taskRepositroy.ToDeleteTask(taskId);

            return Ok("Task Deleted");
        }

    }
}
