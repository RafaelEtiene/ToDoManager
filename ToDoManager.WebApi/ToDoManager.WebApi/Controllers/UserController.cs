using Microsoft.AspNetCore.Mvc;
using ToDoManager.Application.Interfaces;
using ToDoManager.Application.ViewModel;
using ToDoManager.Shared.Exceptions;

namespace ToDoManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("GetUserByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            try
            {
                var result = await _service.GetUserByNameAsync(userName);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest($"An error ocurred during GetUserByName. Error: {ex.Message}");
            }
        }

        [HttpPost("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody]InsertUserViewModel viewModel)
        {
            try
            {
                await _service.InsertUserAsync(viewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred during GetUserByName. Error: {ex.Message}");
            }
        }
    }
}
