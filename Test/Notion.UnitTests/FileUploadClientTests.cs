using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using Newtonsoft.Json;
using Notion.Client;
using Xunit;

namespace Notion.UnitTests;

public class FileUploadClientTests
{
    private readonly AutoMocker _mocker = new();
    private readonly FileUploadsClient _fileUploadClient;
    private readonly Mock<IRestClient> _restClientMock;

    public FileUploadClientTests()
    {
        _restClientMock = _mocker.GetMock<IRestClient>();
        _fileUploadClient = _mocker.CreateInstance<FileUploadsClient>();
    }

    [Fact]
    public async Task CreateAsync_ThrowsArgumentNullException_WhenRequestIsNull()
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _fileUploadClient.CreateAsync(null));
        Assert.Equal("fileUploadObjectRequest", exception.ParamName);
        Assert.Equal("Value cannot be null. (Parameter 'fileUploadObjectRequest')", exception.Message);
    }

    [Fact]
    public async Task CreateAsync_CallsRestClientPostAsync_WithCorrectParameters()
    {
        // Arrange
        var request = new CreateFileUploadRequest
        {
            FileName = "testfile.txt",
            Mode = FileUploadMode.SinglePart,
        };

        var expectedResponse = new CreateFileUploadResponse
        {
            UploadUrl = "https://example.com/upload",
            Id = Guid.NewGuid().ToString(),
        };

        _restClientMock
            .Setup(client => client.PostAsync<CreateFileUploadResponse>(
                It.Is<string>(url => url == ApiEndpoints.FileUploadsApiUrls.Create()),
                It.IsAny<ICreateFileUploadBodyParameters>(),
                It.IsAny<IEnumerable<KeyValuePair<string, string>>>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<JsonSerializerSettings>(),
                It.IsAny<IBasicAuthenticationParameters>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var response = await _fileUploadClient.CreateAsync(request);

        // Assert
        Assert.Equal(expectedResponse.UploadUrl, response.UploadUrl);
        Assert.Equal(expectedResponse.Id, response.Id);

        _restClientMock.VerifyAll();
    }
}