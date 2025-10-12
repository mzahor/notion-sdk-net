using System.Threading.Tasks;
using Notion.Client;
using Xunit;

namespace Notion.IntegrationTests
{
    public class FileUploadsClientTests : IntegrationTestBase
    {
        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var request = new CreateFileUploadRequest
            {
                Mode = FileUploadMode.ExternalUrl,
                ExternalUrl = "https://unsplash.com/photos/hOhlYhAiizc/download?ixid=M3wxMjA3fDB8MXxhbGx8fHx8fHx8fHwxNzYwMTkxNzc3fA&force=true",
                FileName = "sample-image.jpg",
            };

            // Act
            var response = await Client.FileUploads.CreateAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Status);
            Assert.Equal("sample-image.jpg", response.FileName);
        }
    }
}