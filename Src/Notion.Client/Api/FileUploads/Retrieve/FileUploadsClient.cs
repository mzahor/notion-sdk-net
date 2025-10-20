using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notion.Client
{
    public sealed partial class FileUploadsClient
    {
        public async Task<RetrieveFileUploadResponse> RetrieveAsync(
            RetrieveFileUploadRequest request,
            CancellationToken cancellationToken = default)
        {
            IRetrieveFileUploadPathParameters pathParameters = request;

            if (pathParameters == null || string.IsNullOrEmpty(pathParameters.FileUploadId))
            {
                throw new ArgumentNullException(nameof(pathParameters.FileUploadId), "FileUploadId cannot be null or empty.");
            }

            var endpoint = ApiEndpoints.FileUploadsApiUrls.Retrieve(pathParameters);

            return await _restClient.GetAsync<RetrieveFileUploadResponse>(
                endpoint,
                cancellationToken: cancellationToken
            );
        }
    }
}
