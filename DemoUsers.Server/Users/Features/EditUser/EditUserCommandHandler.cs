using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Dtos;
using MediatR;

namespace DemoUsers.Server.Users.Features.EditUser
{
    public record EditUserCommand(User User) : IRequest<bool>;
    internal class EditUserCommandHandler
    {
        readonly ILogger _logger;
        readonly IUsersRepository _usersRepository;

        public EditUserCommandHandler(ILogger<EditUserCommandHandler> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        public async Task<bool> Handle(EditUserCommand command, CancellationToken cancellationToken)
        {
            return await _usersRepository.UpdateUserAsync(command.User, cancellationToken);
        }
    }
}
