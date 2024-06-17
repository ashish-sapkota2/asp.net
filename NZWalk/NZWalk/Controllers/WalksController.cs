using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.CustomActionFilters;
using NZWalk.Models;
using NZWalk.Models.DTO;
using NZWalk.Repositories;

namespace NZWalk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper maper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper maper, IWalkRepository walkRepository)
        {
            this.maper = maper;
            this.walkRepository = walkRepository;
        }
        //create walk
        //POST: //api/walks
        [HttpPost]
        [validateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

                // map dto to domain model
                var walkdomainmodel = maper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkdomainmodel);

                //map domain model to dto


                return Ok(maper.Map<WalkDto>(walkdomainmodel));
           
        }
        //GET Walks
        //GET: /api/walks?filterOn=Name&filterQuery=TracksortBy=Name&isAscending=true

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending
            )
        {
            var walkdomainmodel=await walkRepository.GetAllAsync(filterOn,filterQuery, sortBy, isAscending?? true);

            //map domain model to dto

            return Ok(maper.Map<List<WalkDto>>(walkdomainmodel));
        }

        //Get Walk By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult>GetById(Guid id)
        { 
            var walkdomainmodel = await walkRepository.GetByIdAsync(id);

            if (walkdomainmodel == null)
            {
                return NotFound();
            }
            //map domain model to dto

            return Ok(maper.Map<WalkDto>(walkdomainmodel));
        }

        //PUT walk
        [HttpPut("{id:guid}")]
        [validateModel]

        public async Task<IActionResult> Update(Guid id,UpdateWalkRequestDto updateWalkRequestDto) 
        {
                //map DTO to domain model
                var walkDomainmodel = maper.Map<Walk>(updateWalkRequestDto);
                await walkRepository.UpdateAsync(id, walkDomainmodel);

                if (walkDomainmodel == null)
                {
                    return NotFound();
                }
                //map domain model to DTO

                return Ok(maper.Map<WalkDto>(walkDomainmodel));
            
         }

        //Delete Walk
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
           
           var deletedmodel= await walkRepository.DeleteAsync(id);
            if(deletedmodel == null)
            {
                return NotFound();
            }
            //map domain model to DTO
            return Ok(maper.Map<WalkDto>(deletedmodel));
        }
    }
}
