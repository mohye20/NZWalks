using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repository;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalkController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalksRequestDto walkCreateRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Walk walkDomainModel = _mapper.Map<Walk>(walkCreateRequestDto);
            var walk = await _walkRepository.CreateAsync(walkDomainModel);


            return Ok(_mapper.Map<WalkDto>(walk));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Walk> walksDomainModel = await _walkRepository.GetAllAsync();
            var walksDto = _mapper.Map<List<WalkDto>>(walksDomainModel);
            return Ok(walksDto);
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(id);

            if (walkDomainModel is null)
            {
                return NotFound("No walk found");
            }

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,
            [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Walk walk = _mapper.Map<Walk>(updateWalkRequestDto);
            var walkDomainModel = await _walkRepository.UpdateAsync(id, walk);
            if (walkDomainModel is null)
            {
                return NotFound("Walk not found");
            }

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Walk walkDomainModel = await _walkRepository.DeleteAsync(id);
            if (walkDomainModel is null)
            {
                return NotFound("Walk Not Found");
            }

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}