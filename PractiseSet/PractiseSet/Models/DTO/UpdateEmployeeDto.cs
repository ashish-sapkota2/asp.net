using System.ComponentModel.DataAnnotations;

namespace PractiseSet.Models.DTO
{
    public class UpdateEmployeeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }
    }
}
