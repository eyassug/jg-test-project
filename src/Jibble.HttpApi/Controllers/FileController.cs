using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Jibble.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        // POST api/files
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue)] // 500Mb
        [RequestSizeLimit(209715200)]
        public async Task<IActionResult> PostAsync(IFormFile file, CancellationToken cancellationToken)
        {
            if (file.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                Console.WriteLine($"Uploading {fileName}");
                var filePath = Path.Combine("files", Path.GetFileName(file.FileName));

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream, cancellationToken);
                }
                return Ok(fileName);
            }
            throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
