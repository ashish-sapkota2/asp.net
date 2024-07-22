using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.Models;
using NZWalk.Models.DTO;
using NZWalk.Repositories;

namespace NZWalk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {

            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        //POST: /api/Images/Upload
        public async Task<IActionResult> Upload([FromForm] ImageUploadeRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid) 
            {
                //convert DTo to domain model

                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInByte =request.File.Length,
                    FileName = request.FileName,
                    FileDescription =request.FileDescription,
                };

                //user repository to upload image

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);


            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadeRequestDto request)
        {
            var allowedExtensions = new string[] {
                ".JPG",
                ".jpeg",
                ".ng"
            };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)) == false)
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10MB, please upload a similiar size file");
            }

        }
    }
}
