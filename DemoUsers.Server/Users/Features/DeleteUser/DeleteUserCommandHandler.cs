using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Features.Create;
using MediatR;

namespace DemoUsers.Server.Users.Features.DeleteUser
{
    public record DeleteUserCommand(int Id) : IRequest<bool>;

    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        readonly ILogger<DeleteUserCommandHandler> _logger;
        readonly IUsersRepository _usersRepository;

        public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(DeleteUserCommandHandler)}] deleting user: {request.Id}");
            try
            {
                var user = await _usersRepository.GetUserAsync(request.Id, cancellationToken);
                if (user == null)
                {
                    _logger.LogWarning($"[{nameof(DeleteUserCommandHandler)}] user not found: {request.Id}");
                    return false;
                }

                var result = await _usersRepository.DeleteUserAsync(request.Id, cancellationToken);
                if (result)
                    _logger.LogInformation($"[{nameof(DeleteUserCommandHandler)}] user deleted: {request.Id}");
                else
                    _logger.LogWarning($"[{nameof(DeleteUserCommandHandler)}] user not deleted: {request.Id}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(DeleteUserCommandHandler)}] Error occurred while getting user: {request.Id}");
                throw;
            }
            
        }
    }
}
