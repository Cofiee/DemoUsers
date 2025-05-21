using DemoUsers.Server.Users.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoUsers.Server.Users
{
    public static class UsersStartupExtension
    {
        public static void AddUsersFeature(this IServiceCollection services)
        {
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseInMemoryDatabase("Demo");
            });

            services.AddTransient<IUsersRepository, UsersRepository>();
        }

        public static void AddExampleUsers(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
            dbContext.Database.EnsureCreated();
            if (!dbContext.Users.Any())
            {
                dbContext.Users.Add(new UserEntity { Name = "John Doe", Email = "aaa@aa.pl", Image = new Uri("https://png.pngtree.com/png-vector/20191121/ourmid/pngtree-blue-bird-vector-or-color-illustration-png-image_2013004.jpg") });
                dbContext.Users.Add(new UserEntity { Name = "Josh Doe", Email = "bbbb@aa.pl" });
                dbContext.SaveChanges();
            }
        }
    }
}
