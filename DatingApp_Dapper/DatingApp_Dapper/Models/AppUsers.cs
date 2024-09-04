namespace DatingApp_Dapper.Models
{
    public class AppUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive {  get; set; }= DateTime.UtcNow;
        public string? Gender { get; set; }
        public string ? Introduction { get; set; }
        public string? Interest { get; set; }
        public string? City {  get; set; }
        public string? Country { get; set; }

        // Navigation property to represent the one-to-many relationship
        public ICollection<Photo>? Photos { get; set; }


    }
}
