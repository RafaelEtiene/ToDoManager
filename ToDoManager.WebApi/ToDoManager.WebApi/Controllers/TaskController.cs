using Microsoft.AspNetCore.Mvc;
using ToDoManager.Application.Interfaces;
using ToDoManager.Application.ViewModel;
using ToDoManager.Shared.Exceptions;

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
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest($"An error ocurred during GetTasks. Error: {ex.Message}");
            }
        }
    }
}
