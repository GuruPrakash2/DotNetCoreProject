using Microsoft.EntityFrameworkCore;
using NGWalks.API.Data;
using NGWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NzWalksDbContext dbContext;
        public SQLWalkRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, int pageNumber=1, int pageSize=1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                
            } 

            //NAvigation property is used to get the related data from the other table. In this case, we are getting the related data from the Difficulty and Region tables.
            //var walks = await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            //return walks;

            //pagination
            var skipResults = (pageNumber - 1) * pageSize;

            //sorting
            return await walks.OrderByDescending(x=>x.Name).Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
