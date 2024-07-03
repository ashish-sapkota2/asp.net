namespace Contactly.Models
{
    public class AddRequestDto
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string Phone { get; set; }
        public bool Favourite { get; set; }
    }
}
