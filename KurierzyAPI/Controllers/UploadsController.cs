using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

namespace KurierzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly ILogger<UploadsController> _logger;
        public UploadsController(ILogger<UploadsController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            //process file content
            
            string[] subs = file.FileName.Split('.');
            string extension = subs[subs.Length - 1];

            string[] allowedImageTypes = {"png", "jpg", "jpeg"};
            if (allowedImageTypes.Contains(extension)){
                string saveFolder = "../userFiles/";
                string uniqueFilePath = Guid.NewGuid().ToString();
                using (var stream = System.IO.File.Create(saveFolder + uniqueFilePath + "." + extension))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok($"Saved file {file.FileName} as {uniqueFilePath}");
            }
            return BadRequest("Unsupported media type");
            //return BadRequest("Only allowed media types are: " + allowedImageTypes.ToString(), 415);
        }
    }
}
