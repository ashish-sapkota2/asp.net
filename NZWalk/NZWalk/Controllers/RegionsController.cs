using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.Data;
using NZWalk.Models;
using NZWalk.Models.DTO;
using static System.Net.WebRequestMethods;

namespace NZWalk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext=dbContext;
        }

        [HttpGet]   
        public IActionResult GetAll()
        {
            //Get data from database == domain models

            var regionsDomain=dbContext.Regions.ToList();

            //map domain models to DTOs

            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain) {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            //Return Dtos
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id: Guid}")]
        public IActionResult Get([FromRoute]Guid id)
        {
            //get region domain model from db
            var region = dbContext.Regions.Find(id);
            // var region = dbContext.Regions.FirstOrDefault(x => x.id==id);

            if (region == null) { 
                return NotFound();
            }
            //map domain model to dto
            var regionsDto = new RegionDto{
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
               RegionImageUrl = region.RegionImageUrl,
            }; 

            return Ok(regionsDto);  
        }
    }
}
