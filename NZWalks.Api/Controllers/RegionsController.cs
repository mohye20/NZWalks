using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllRegions()
        {
            var regions = _dbContext.Regions.ToList();
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
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.Find(id);
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
        public IActionResult CreateRegion([FromBody] CreateRegionRequestDto createRegionRequestDto)
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
        public IActionResult UpdateRegion(Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var region = _dbContext.Regions.Find(id);
            if (region is null)
            {
                return NotFound();
            }

            region.Name = updateRegionRequestDto.Name;
            region.Code = updateRegionRequestDto.Code;
            region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            _dbContext.SaveChanges();

            var regionDto = new RegionDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}