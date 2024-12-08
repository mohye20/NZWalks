using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public WalkController(IMapper mapper , IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalksRequestDto walkCreateRequestDto)
        {
            Walk walkDomainModel = _mapper.Map<Walk>(walkCreateRequestDto);
            var walk = await _walkRepository.CreateAsync(walkDomainModel);
            
            
            return Ok(_mapper.Map<WalkDto>(walk));
        }
    }
}