using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Dtos;
using MediatR;

namespace DemoUsers.Server.Users.Features.Create
{
    public record CreateUserCommand(User User) : IRequest<int>;

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        readonly ILogger _logger;
        readonly IUsersRepository _usersRepository;

        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken) => 
            await _usersRepository.CreateUserAsync(command.User, cancellationToken);
    }

}
