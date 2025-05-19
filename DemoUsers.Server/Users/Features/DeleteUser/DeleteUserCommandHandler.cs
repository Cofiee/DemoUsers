using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Features.Create;
using MediatR;

namespace DemoUsers.Server.Users.Features.DeleteUser
{
    public record DeleteUserCommand(int Id) : IRequest<bool>;

    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        readonly ILogger _logger;
        readonly IUsersRepository _usersRepository;

        public DeleteUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _usersRepository.DeleteUserAsync(request.Id, cancellationToken);
        }
    }
}
