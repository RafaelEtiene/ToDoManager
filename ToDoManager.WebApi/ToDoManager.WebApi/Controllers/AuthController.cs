using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoManager.Application.Interfaces;
using ToDoManager.Application.ViewModel;
using ToDoManager.Shared.Exceptions;

namespace ToDoManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody]LoginViewModel viewModel)
        {
            try
            {
                await _service.RegisterAsync(viewModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred during RegisterUser. Error: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            try
            {
                var result = await _service.GenerateJwtToken(viewModel);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized($"Unauthorized. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred during Login. Error: {ex.Message}");
            }
        }
    }
}
