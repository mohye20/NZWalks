using AutoMapper;
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
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllAsync();


            return Ok(_mapper.Map<List<RegionDTO>>(regions));
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


            return Ok(_mapper.Map<RegionDTO>(region));
        }


        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            var region = _mapper.Map<Region>(createRegionRequestDto);

            await _regionRepository.CreateAsync(region);


            var regionDto = _mapper.Map<RegionDTO>(region);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion(Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDominModel =  _mapper.Map<Region>(updateRegionRequestDto);
            var region = await _regionRepository.UpdateAsync(id, regionDominModel);
            if (region is null)
            {
                return NotFound();
            }


            var regionDto = _mapper.Map<RegionDTO>(region);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);
            if (region is null)
            {
                return NotFound();
            }


            return Ok("Region deleted Done");
        }
    }
}