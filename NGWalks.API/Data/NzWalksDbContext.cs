using Microsoft.EntityFrameworkCore;
using NGWalks.API.Models.Domain;

namespace NGWalks.API.Data
{
    public class NzWalksDbContext: DbContext
    {
        public NzWalksDbContext(DbContextOptions<NzWalksDbContext> options) : base(options)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
