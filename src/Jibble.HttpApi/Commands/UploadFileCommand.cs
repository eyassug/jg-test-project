using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Jibble.Commands
{
    public class UploadFileCommand : IRequest<string>
    {
        public IFormFile File { get; set; }
    }

    public class FileHandler : IRequestHandler<UploadFileCommand, string>
    {
        IBackgroundJobClient BackgroundWorker { get; }
        Services.ICSVImportService ImportService { get; }
        public FileHandler(Services.ICSVImportService importService, IBackgroundJobClient backgroundJobClient)
        {
            ImportService = importService;
            BackgroundWorker = backgroundJobClient;
        }
        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var folderName = Path.Combine("files", $"{Guid.NewGuid()}");
            if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);
            var fileName = Path.Combine(folderName, Path.GetFileName(request.File.FileName));
            Console.WriteLine($"Uploading {fileName}");
            var filePath = fileName;
            using (var stream = System.IO.File.Create(filePath))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }
            Console.WriteLine($"Completed upload");
            BackgroundWorker.Enqueue(() => ImportService.ProcessAsync(folderName, default));
            return folderName;
        }
    }
}
