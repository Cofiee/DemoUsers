using DemoUsers.Server.Users.Dtos;
using DemoUsers.Server.Users.Features.Create;
using DemoUsers.Server.Users.Features.DeleteUser;
using DemoUsers.Server.Users.Features.EditUser;
using DemoUsers.Server.Users.Features.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoUsers.Server.Users
{
    //[ApiVersion("1.0")]
    //[Route("/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _mediator.Send(new GetUserDetailsQuery(id));
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var userId = await _mediator.Send(new CreateUserCommand(user));
            return CreatedAtAction(nameof(GetUser), new { id = userId }, null);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
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
