using Microsoft.AspNetCore.Mvc;

namespace ToDoManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet("GetTasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                return Ok(await _service.GetTasksAsync());
            }
            catch(Exception ex)
            {
                return BadRequest($"An error ocurred during GetTasks. Error: {ex.Message}");
            }
        }
    }
}
