using NGWalks.API.Models.Domain;

namespace NZWalks.API.Models.DT
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public string Description { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
