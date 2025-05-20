/* W data acces layer od razu używam abstrakcji ponieważ,
 * jakiekolwiek zmiany tehchnologiczne są najbardziej newralgiczne na poziomie bazy danych
 * Zmiana np z SQL na Mongo albo Influx etc będzie wyjątkowo problematyczna,
 * kiedy zależności bazodanowe wyjdą poza repozytoria.
 */

using AutoMapper;
using DemoUsers.Server.Users.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DemoUsers.Server.Users.Data
{
    internal class UsersRepository : IUsersRepository
    {
        readonly UsersDbContext _dbContext;
        readonly IMapper _mapper;

        public UsersRepository(UsersDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            var entity = new UserEntity
            {
                Name = user.Name,
                Email = user.Email,
                Image = user.Image
            };
            _dbContext.Users.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken); 
            
            return _mapper.Map<User>(user);
        }

        public async Task<IEnumerable<UserSimple>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _mapper
                .ProjectTo<UserSimple>(_dbContext.Users.AsNoTracking(), null)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users.FindAsync(user.Id);
            if (entity == null)
            {
                return false;
            }

            entity.Name = user.Name;
            entity.Email = user.Email;
            entity.Image = user.Image;
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
