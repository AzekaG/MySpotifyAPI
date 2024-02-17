namespace MySpotifyAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }


        public string? FirstName { get; set; }


        public string? LastName { get; set; }


        public string? Email { get; set; }


        public bool Active { get; set; }


        public string? Password { get; set; }

        public string? UserStatus { get; set; }

        public string? Salt {  get; set; }
    }
}
