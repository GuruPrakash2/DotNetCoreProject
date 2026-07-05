using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DT
{
    public class RegionRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Code must be at least 3 characters long.")]
        [MaxLength(3, ErrorMessage = "Code cannot be longer than 3 characters.")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
