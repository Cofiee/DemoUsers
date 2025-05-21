using DemoUsers.Server.Users.Dtos;
using DemoUsers.Server.Users.Features.Create;
using DemoUsers.Server.Users.Features.DeleteUser;
using DemoUsers.Server.Users.Features.EditUser;
using DemoUsers.Server.Users.Features.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoUsers.Server.Users
{
    /* Korzystam z wersjonowanego api, przyszłościowo
     * w pliku vite.config.js react mapuje ścieżki '/users' na '/api/v1.0/users'
     * Dzięki temu komponenty frontendu nie wiedzą do której wersji się odwołują
     */
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _mediator.Send(new GetUserDetailsQuery(id));
            if (user == null)
                return BadRequest("User not found.");

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var userId = await _mediator.Send(new CreateUserCommand(user));
            if (userId == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, "User not created.");

            return Created();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            user.Id = id;
            await _mediator.Send(new EditUserCommand(user));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}
