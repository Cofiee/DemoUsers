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

        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(CreateUserCommandHandler)}] creating user: {command.User.Name}, email: {command.User.Email}");

            try
            {
                var userId = await _usersRepository.CreateUserAsync(command.User, cancellationToken);
                if (userId == 0)
                    _logger.LogWarning($"[{nameof(CreateUserCommandHandler)}] user not created: {command.User.Name}, email: {command.User.Email}");
                else
                    _logger.LogInformation($"[{nameof(CreateUserCommandHandler)}] user created with Id: {userId}");

                return userId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(CreateUserCommandHandler)}] Error occurred while creating user: {command.User.Name}, email: {command.User.Email}");
                throw;
            }
        }
    }

}
