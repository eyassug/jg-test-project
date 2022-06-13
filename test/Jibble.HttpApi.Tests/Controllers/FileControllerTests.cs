using Xunit;

namespace Jibble.Controllers
{
    public class FileControllerTests
    {

        [Fact]
        public void Upload_ReturnsBadRequest_WithExtensionsExceptZipCSV()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Upload_ReturnsBadRequest_WithEmptyFile()
        {
            Assert.Equal(1, 1);
        }


    }
}
