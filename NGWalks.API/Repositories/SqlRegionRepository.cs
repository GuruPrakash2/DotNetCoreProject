using Microsoft.EntityFrameworkCore;
using NGWalks.API.Data;
using NGWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NzWalksDbContext dbContext;
        public SqlRegionRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
