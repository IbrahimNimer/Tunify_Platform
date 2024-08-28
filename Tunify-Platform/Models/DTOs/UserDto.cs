namespace Tunify_Platform.Models.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; set; }

    }
}
