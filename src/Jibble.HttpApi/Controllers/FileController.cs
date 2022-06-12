using MediatR;
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
        Services.ICSVImportService ImportService { get; }
        IMediator Mediator { get; }
        public FileController(Services.ICSVImportService cSVImportService, IMediator mediator)
        {
            ImportService = cSVImportService;
            Mediator = mediator;
        }
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
                var extension = Path.GetExtension(file.FileName);
                if (!extension.Equals(".zip") && !extension.Equals(".csv"))
                    throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest, "Invalid file type");

                var folderName = await Mediator.Send(new Commands.UploadFileCommand { File = file }, cancellationToken);
                return Ok(folderName);
            }
            throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
