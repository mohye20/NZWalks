using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repository;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllAsync();
            var regionsDto = new List<RegionDTO>();

            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            return Ok(regionsDto);
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetByIdAsync(id);
            if (region is null)
            {
                return NotFound();
            }

            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(regionDTO);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            var region = new Region()
            {
                Name = createRegionRequestDto.Name,
                Code = createRegionRequestDto.Code,
                RegionImageUrl = createRegionRequestDto.RegionImageUrl
            };

            await _regionRepository.CreateAsync(region);


            var regionDto = new RegionDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion(Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDominModel = new Region()
            {
                Name = updateRegionRequestDto.Name,
                Code = updateRegionRequestDto.Code,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
            var region = await _regionRepository.UpdateAsync(id, regionDominModel);
            if (region is null)
            {
                return NotFound();
            }


            var regionDto = new RegionDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region =await _regionRepository.DeleteAsync(id);
            if (region is null)
            {
                return NotFound();
            }


            return Ok("Region deleted Done");
        }
    }
}