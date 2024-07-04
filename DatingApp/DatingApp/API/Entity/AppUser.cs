namespace DatingApp.Entity
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public byte []  PasswordHash { get; set; }
        public byte[] Passwordsalt { get; set; }

    }
}
