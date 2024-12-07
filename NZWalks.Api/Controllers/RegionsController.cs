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
            Console.WriteLine(Guid.NewGuid());
            var regionDTO = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionDTO);
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
    }
}