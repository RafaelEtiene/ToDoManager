using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("GetTasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var result = await _service.GetTasksAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest($"An error ocurred during GetTasks. Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("InsertTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertTask([FromBody]InsertTaskViewModel viewModel)
        {
            try
            {
                await _service.InsertTaskAsync(viewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred during GetTasks. Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("UpdateStateTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStateTask([FromBody] Guid idTask, bool isCompleted)
        {
            try
            {
                await _service.UpdateStateTaskAsync(idTask, isCompleted);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred during UpdateStateTask. Error: {ex.Message}");
            }
        }
    }
}
