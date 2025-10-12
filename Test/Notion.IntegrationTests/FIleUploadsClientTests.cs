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

        [Fact]
        public async Task Verify_file_upload_flow()
        {
            // Arrange
            var createRequest = new CreateFileUploadRequest
            {
                Mode = FileUploadMode.SinglePart,
                FileName = "notion-logo.png",
            };

            var createResponse = await Client.FileUploads.CreateAsync(createRequest);

            var sendRequest = SendFileUploadRequest.Create(
                createResponse.Id,
                new FileData
                {
                    FileName = "notion-logo.png",
                    Data = System.IO.File.OpenRead("assets/notion-logo.png"),
                    ContentType = createResponse.ContentType
                }
            );

            // Act
            var sendResponse = await Client.FileUploads.SendAsync(sendRequest);

            // Assert
            Assert.NotNull(sendResponse);
            Assert.Equal(createResponse.Id, sendResponse.Id);
            Assert.Equal("notion-logo.png", sendResponse.FileName);
            Assert.Equal("uploaded", sendResponse.Status);
        }
    }
}