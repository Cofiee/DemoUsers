/* Używam dbcontextu in memory, bo działa ad hoc
 * Można po prostu zrobić jeszcze prościej przez wykorzystanie zwykłego słownika etc
 */

using Microsoft.EntityFrameworkCore;

namespace DemoUsers.Server.Users.Data
{
    internal class UsersDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }
    }
}
