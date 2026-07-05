using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DT
{
    public class WalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(0,100)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public  string Description { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
    }
}
