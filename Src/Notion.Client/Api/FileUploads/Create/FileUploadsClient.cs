using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notion.Client
{
    public sealed partial class FileUploadsClient : IFileUploadsClient
    {
        public async Task<CreateFileUploadResponse> CreateAsync(
            CreateFileUploadRequest fileUploadObjectRequest,
            CancellationToken cancellationToken = default)
        {
            if (fileUploadObjectRequest == null)
            {
                throw new ArgumentNullException(nameof(fileUploadObjectRequest));
            }

            ICreateFileUploadBodyParameters body = fileUploadObjectRequest;

            return await _restClient.PostAsync<CreateFileUploadResponse>(
                ApiEndpoints.FileUploadsApiUrls.Create(),
                body,
                cancellationToken: cancellationToken
            );
        }
    }
}
