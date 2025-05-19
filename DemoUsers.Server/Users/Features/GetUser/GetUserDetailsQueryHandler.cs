using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Dtos;
using MediatR;

namespace DemoUsers.Server.Users.Features.GetUser
{
    public record GetUserDetailsQuery(int Id) : IRequest<User>;
    internal class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, User>
    {
        readonly ILogger _logger;
        readonly IUsersRepository _usersRepository;

        public GetUserDetailsQueryHandler(ILogger<GetUserDetailsQueryHandler> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        async Task<User> IRequestHandler<GetUserDetailsQuery, User>.Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.GetUserAsync(request.Id, cancellationToken);
        }
    }
}
