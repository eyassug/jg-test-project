using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Jibble.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        // POST api/files
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
