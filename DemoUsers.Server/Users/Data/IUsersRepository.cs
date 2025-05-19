using DemoUsers.Server.Users.Dtos;

namespace DemoUsers.Server.Users.Data
{
    public interface IUsersRepository
    {
        Task<User> GetUserAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<UserSimple>> GetUsersAsync(CancellationToken cancellationToken);
        Task<int> CreateUserAsync(User user, CancellationToken cancellationToken);
        Task<bool> UpdateUserAsync(User user, CancellationToken cancellationToken);
        Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken);
    }
}
