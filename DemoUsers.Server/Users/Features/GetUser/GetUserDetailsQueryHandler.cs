using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Dtos;
using MediatR;

namespace DemoUsers.Server.Users.Features.GetUser
{
    public record GetUserDetailsQuery(int Id) : IRequest<User?>;
    internal class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, User?>
    {
        readonly ILogger _logger;
        readonly IUsersRepository _usersRepository;

        public GetUserDetailsQueryHandler(ILogger<GetUserDetailsQueryHandler> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        async Task<User?> IRequestHandler<GetUserDetailsQuery, User?>.Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting user with id {request.Id}");
            try
            {
                var result = await _usersRepository.GetUserAsync(request.Id, cancellationToken);
                if (result is null)
                    _logger.LogWarning($"User with id {request.Id} not found");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user with id {request.Id}");
                throw;
            }
        }
    }
}
