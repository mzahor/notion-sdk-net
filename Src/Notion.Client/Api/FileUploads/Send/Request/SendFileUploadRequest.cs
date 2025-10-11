using System.IO;
using Newtonsoft.Json;

namespace Notion.Client
{
    public class SendFileUploadRequest : ISendFileUploadFormDataParameters, ISendFileUploadPathParameters
    {
        public FileData File { get; private set; }
        public string PartNumber { get; private set; }
        public string FileUploadId { get; private set; }

        private SendFileUploadRequest() { }

        public static SendFileUploadRequest Create(string fileUploadId, FileData file, string partNumber = null)
        {
            return new SendFileUploadRequest
            {
                FileUploadId = fileUploadId,
                File = file,
                PartNumber = partNumber
            };
        }
    }

    public interface ISendFileUploadFormDataParameters
    {

        /// <summary>
        /// The raw binary file contents to upload.
        /// </summary>
        FileData File { get; }

        /// <summary>
        /// When using a mode=multi_part File Upload to send files greater than 20 MB in parts, this is the current part number. 
        /// Must be an integer between 1 and 1000 provided as a string form field.
        /// </summary>
        [JsonProperty("part_number")]
        string PartNumber { get; }
    }

    public class FileData
    {
        /// <summary>
        /// The name of the file being uploaded.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The content of the file being uploaded.
        /// </summary>
        public Stream Data { get; set; }

        /// <summary>
        /// The MIME type of the file being uploaded.
        /// </summary>
        public string ContentType { get; set; }
    }

    public interface ISendFileUploadPathParameters
    {
        /// <summary>
        /// The `file_upload_id` obtained from the `id` of the Create File Upload API response.
        /// </summary>
        string FileUploadId { get; }
    }
}