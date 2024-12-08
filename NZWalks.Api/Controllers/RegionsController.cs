using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _dbContext.Regions.ToListAsync();
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
            var region =await _dbContext.Regions.FindAsync(id);
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
            _dbContext.Regions.Add(region);
            _dbContext.SaveChanges();

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
            var region =await _dbContext.Regions.FindAsync(id);
            if (region is null)
            {
                return NotFound();
            }

            region.Name = updateRegionRequestDto.Name;
            region.Code = updateRegionRequestDto.Code;
            region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            await _dbContext.SaveChangesAsync();

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
            var region =await _dbContext.Regions.FindAsync(id);
            if (region is null)
            {
                return NotFound();
            }

            _dbContext.Regions.Remove(region);
            _dbContext.SaveChangesAsync();

            return Ok("Region deleted Done");
        }
    }
}