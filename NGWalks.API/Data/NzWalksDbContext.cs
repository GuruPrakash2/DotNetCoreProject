using Microsoft.EntityFrameworkCore;
using NGWalks.API.Models.Domain;

namespace NGWalks.API.Data
{
    public class NzWalksDbContext : DbContext
    {
        public NzWalksDbContext(DbContextOptions<NzWalksDbContext> options) : base(options)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed data for Difficulties
            //Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("69c9925f-10ad-4f5c-8c60-1963b389fd64"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("80e5a420-b2fb-46b2-82d7-13e4f12bc2ea"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("c46296e9-6719-4508-a372-869cd42ce640"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            // Seed data for Regions

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("387f950e-36c9-4e81-b28c-ea243a6af58c"),
                    Name = "Northland",
                    Code = "NL",
                    RegionImageUrl = "image.url"
                    
                },
                new Region()
                {
                    Id = Guid.Parse("609c5351-3f07-4d88-ba23-9b043998067c"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "image1.url"

                }
            };

            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
