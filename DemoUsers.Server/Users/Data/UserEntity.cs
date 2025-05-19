using System.ComponentModel.DataAnnotations;

namespace DemoUsers.Server.Users.Data
{
    internal class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Uri? Image { get; set; }
    }
}
