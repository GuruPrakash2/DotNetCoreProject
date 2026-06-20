using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NGWalks.API.Models.Domain;
using NZWalks.API.Models.DT;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WalkRequestDto addWalkRequestDto)
        {
            //Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);
            //Map Domain Model back to Dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAllAsync();
            //Map Domain Models to DTOs
            var walksDto = mapper.Map<List<WalkDto>>(walks);
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetWalkByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            //Map Domain Model to DTO
            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] WalkRequestDto updateWalkRequestDto)
        {
            //Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            var updatedWalk = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (updatedWalk == null)
            {
                return NotFound();
            }
            //Map Domain Model back to Dto
            var walkDto = mapper.Map<WalkDto>(updatedWalk);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalk = walkRepository.DeleteAsync(id);
            if (deletedWalk == null)
            {
                return NotFound();
            }
            //Map Domain Model back to Dto
            var walkDto = mapper.Map<WalkDto>(deletedWalk);
            return Ok(walkDto);
        }
    }
}
