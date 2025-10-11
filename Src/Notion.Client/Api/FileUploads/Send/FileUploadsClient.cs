using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notion.Client
{
    public sealed partial class FileUploadsClient
    {
        public async Task<SendFileUploadResponse> SendAsync(
            SendFileUploadRequest sendFileUploadRequest,
            CancellationToken cancellationToken = default)
        {
            if (sendFileUploadRequest == null)
            {
                throw new ArgumentNullException(nameof(sendFileUploadRequest));
            }

            if (string.IsNullOrEmpty(sendFileUploadRequest.FileUploadId))
            {
                throw new ArgumentNullException(nameof(sendFileUploadRequest.FileUploadId));
            }

            var path = ApiEndpoints.FileUploadsApiUrls.Send(sendFileUploadRequest.FileUploadId);

            return await _restClient.PostAsync<SendFileUploadResponse>(
                path,
                formData: sendFileUploadRequest,
                cancellationToken: cancellationToken
            );
        }
    }
}