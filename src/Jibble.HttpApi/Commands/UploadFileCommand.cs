using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jibble.Commands
{
    public class UploadFileCommand : IRequest<string>
    {
        public IFormFile FileInfo { get; set; }
    }

    public class FileHandler : IRequestHandler<UploadFileCommand, string>
    {
        public Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Handled command");
            return Task.FromResult(Guid.NewGuid().ToString());
        }
    }
}
