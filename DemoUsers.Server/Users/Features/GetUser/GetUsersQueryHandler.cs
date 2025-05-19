using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DemoUsers.Server.Users.Features.GetUser
{
    public record GetUsersQuery : IRequest<IEnumerable<UserSimple>>;
    internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserSimple>>
    {
        readonly ILogger _logger;
        readonly IUsersRepository _usersRepository;

        public GetUsersQueryHandler(ILogger<GetUsersQueryHandler> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        public async Task<IEnumerable<UserSimple>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUsersQueryHandler.Handle");
            return await _usersRepository.GetUsersAsync(cancellationToken);
        }
    }
}
