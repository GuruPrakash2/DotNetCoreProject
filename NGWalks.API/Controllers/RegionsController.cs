using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGWalks.API.Data;
using NGWalks.API.Models.Domain;
using NZWalks.API.Models.DT;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        public RegionsController(NzWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database -> domain models
            var regions = await regionRepository.GetAllAsync();
            //Now need to map that domain data to DTO
            var regionDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            //return DTOs
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get region from database(domain model)
            var region = await regionRepository.GetAsync(id);

            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }
            //Map domain model to DTO
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };
            //Return DTO
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegionRequestDto regionRequestDto)
        {
            var region = new Region
            {
                Id = Guid.NewGuid(),
                Name = regionRequestDto.Name,
                Code = regionRequestDto.Code,
                RegionImageUrl = regionRequestDto.RegionImageUrl
            };

            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = region.Id }, regionDto.Id);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RegionRequestDto regionRequestDto)
        {
            var region = await  dbContext.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            region.Code = regionRequestDto.Code;
            region.Name = regionRequestDto.Name;
            region.RegionImageUrl = regionRequestDto.RegionImageUrl;

           await dbContext.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
