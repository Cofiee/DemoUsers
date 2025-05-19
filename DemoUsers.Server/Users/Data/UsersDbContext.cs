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
