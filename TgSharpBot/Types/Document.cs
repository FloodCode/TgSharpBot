using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// This class represents a general file (as opposed to photos, voice messages and audio files)
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Unique file identifier
        /// </summary>
        [JsonProperty("file_id")]
        public string FileId { get; set; }
        /// <summary>
        /// Optional. Document thumbnail as defined by sender
        /// </summary>
        [JsonProperty("thumb")]
        public PhotoSize Thumb { get; set; }
        /// <summary>
        /// Optional. Original filename as defined by sender
        /// </summary>
        [JsonProperty("file_name")]
        public string FileName { get; set; }
        /// <summary>
        /// Optional. MIME type of the file as defined by sender
        /// </summary>
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
        /// <summary>
        /// Optional. File size
        /// </summary>
        [JsonProperty("file_size")]
        public int FileSize { get; set; }
    }
}
