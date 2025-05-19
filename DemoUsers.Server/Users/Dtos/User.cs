namespace DemoUsers.Server.Users.Dtos
{
    public record User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Uri? Image { get; set; }
    }
}
