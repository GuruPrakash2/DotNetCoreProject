using NGWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        
        Task<Region> GetAsync(Guid id);
    }
}
