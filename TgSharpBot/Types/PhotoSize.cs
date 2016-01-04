using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// This object represents one size of a photo or a file / sticker thumbnail
    /// </summary>
    public class PhotoSize
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        [JsonProperty("file_id")]
        public string FileId { get; set; }
        /// <summary>
        /// Photo width
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }
        /// <summary>
        /// Photo height
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }
        /// <summary>
        /// Optional. File size
        /// </summary>
        [JsonProperty("file_size")]
        public int FileSize { get; set; }
    }
}
