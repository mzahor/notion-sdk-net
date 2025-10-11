using System.Threading;
using System.Threading.Tasks;

namespace Notion.Client
{
    public interface IFileUploadsClient
    {
        /// <summary>
        /// Use this API to initiate the process of uploading a file to your Notion workspace.
        /// </summary>
        /// <param name="fileUploadObjectRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CreateFileUploadResponse> CreateAsync(
            CreateFileUploadRequest fileUploadObjectRequest,
            CancellationToken cancellationToken = default
        );
    }
}