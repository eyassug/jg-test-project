using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Jibble.Controllers
{
    public class FileControllerTests
    {
        readonly FileController controller;
        readonly IMediator mediator;
        public FileControllerTests()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<Commands.UploadFileCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult($"files/{System.Guid.NewGuid()}"));
            mediator = mockMediator.Object;
            controller = new FileController(mockMediator.Object);
        }

        [Fact]
        public async Task Upload_ReturnsBadRequest_WithExtensionsExceptZipCSV()
        {
            // Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var sut = new FileController(mediator);

            //Act
            await Assert.ThrowsAsync<HttpResponseException>(() => sut.PostAsync(file, default));
        }

        [Theory]
        [InlineData(".txt")]
        [InlineData(".jpg")]
        [InlineData(".rar")]
        [InlineData(".xlsx")]
        public async Task Upload_ReturnsBadRequest_WithEmptyFile(string ext)
        {
            // Setup mock file using a memory stream
            var content = "";
            var fileName = $"test{ext}";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var sut = new FileController(mediator);

            //Act
            await Assert.ThrowsAsync<HttpResponseException>(() => sut.PostAsync(file, default));
        }

        [Theory]
        [InlineData(".csv")]
        [InlineData(".zip")]
        public async Task Upload_Success_WithCsvAndZipFiles(string ext)
        {
            // Setup mock file using a memory stream
            var content = "email,phone";
            var fileName = $"test{ext}";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var sut = new FileController(mediator);

            var result = await sut.PostAsync(file, default) as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Upload_Success_WithZipFile()
        {
            // Setup mock file using a memory stream
            var content = "xxx";
            var fileName = "test.zip";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var sut = new FileController(mediator);

            //Act
            var result = await sut.PostAsync(file, default) as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

    }
}
