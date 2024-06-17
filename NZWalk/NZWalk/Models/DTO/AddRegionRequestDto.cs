
using System.ComponentModel.DataAnnotations;

namespace NZWalk.Models.DTO
{
    public class AddRegionRequestDto
    {

        [Required]
        [MinLength(2, ErrorMessage ="Code Has to be a minimum of 2 characters")]
        [MaxLength(3 , ErrorMessage = "Code Has to be a maximum of 3 character")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name Has to be a maximum of 100 character")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; } = null;
    }
}
