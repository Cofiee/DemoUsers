using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Dtos;
using MediatR;

namespace DemoUsers.Server.Users.Features.EditUser
{
    public record EditUserCommand(User User) : IRequest<bool>;
    internal class EditUserCommandHandler : IRequestHandler<EditUserCommand, bool>
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
            _logger.LogInformation($"[{nameof(EditUserCommandHandler)}] editing user: {command.User.Id}");
            try
            {
                var user = await _usersRepository.GetUserAsync(command.User.Id, cancellationToken);
                if (user == null)
                {
                    _logger.LogWarning($"[{nameof(EditUserCommandHandler)}] user not found: {command.User.Id}");
                    return false;
                }

                var result = await _usersRepository.UpdateUserAsync(command.User, cancellationToken);
                if (result)
                    _logger.LogInformation($"[{nameof(EditUserCommandHandler)}] user edited: {command.User.Id}");
                else
                    _logger.LogWarning($"[{nameof(EditUserCommandHandler)}] user not edited: {command.User.Id}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(EditUserCommandHandler)}] Error occurred while editing user: {command.User.Id}");
                throw;
            }
        }
    }
}
