using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NZWalk.CustomActionFilters;
using NZWalk.Data;
using NZWalk.Models;
using NZWalk.Models.DTO;
using NZWalk.Repositories;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;

namespace NZWalk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles ="Rea/*der")]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database == domain models

            var regionsDomain = await regionRepository.GetAllAsync();

            //map domain models to DTOs

            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regionsDomain) {
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}

            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            //Return Dtos
            return Ok(regionsDto);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles ="Reader")]
        //[Route("{id: Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            //get region domain model from db
            var region = await dbContext.Regions.FindAsync(id);
            // var region = dbContext.Regions.FirstOrDefault(x => x.id==id);

            if (region == null) {
                return NotFound();
            }
            //map domain model to dto
            //var regionsDto = new RegionDto {
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl,
            //};

            return Ok(mapper.Map<RegionDto>(region));
        }

        [HttpPost]
        [validateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) {

            //    //map or convert the DTO to domain model
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,

            //};

           
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //use Domain model to create reion
                await dbContext.Regions.AddAsync(regionDomainModel);
                await dbContext.SaveChangesAsync();
                //map domain model back to DTo
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(Get), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut("{id:guid}")]
        [validateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Update(Guid id,[FromBody] UpdateRegionRequestDto updateRegionRequestDto) {
            
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
                //var regionDomainModel= await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                regionDomainModel.Code = updateRegionRequestDto.Code;
                regionDomainModel.Name = updateRegionRequestDto.Name;
                regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
                await dbContext.SaveChangesAsync();

                //convet domainmodel to DTo

                //var regionDto = new RegionDto
                //{
                //    Id= regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl= regionDomainModel.RegionImageUrl,
                //};

                var regionDto = mapper.Map<Region>(regionDomainModel);
                return Ok(regionDto);
 
          
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(y => y.Id == id);
            var regionDomainModel = mapper.Map<Region>(id);
            if (regionDomainModel == null) {
                return NotFound();
            }
             dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();


            return Ok("Successfully Deleted");
        }
        
    }
}
