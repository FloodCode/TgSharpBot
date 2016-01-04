using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// Represents a link to a photo
    /// By default, this photo will be sent by the user with optional caption
    /// Alternatively, you can provide message_text to send it instead of photo
    /// </summary>
    public class InlineQueryResultPhoto : InlineQueryResult
    {
        /// <summary>
        /// A valid URL of the photo. Photo size must not exceed 5MB
        /// </summary>
        [JsonProperty("photo_url")]
        public string PhotoUrl { get; set; }
        /// <summary>
        /// Optional. MIME type of the photo, defaults to image/jpeg
        /// </summary>
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
        /// <summary>
        /// Optional. Width of the photo
        /// </summary>
        [JsonProperty("photo_width")]
        public int PhotoWidth { get; set; }
        /// <summary>
        /// Optional. Height of the photo
        /// </summary>
        [JsonProperty("photo_height")]
        public int PhotoHeight { get; set; }
        /// <summary>
        /// URL of the thumbnail for the photo
        /// </summary>
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Optional. Caption of the photo to be sent, 0-200 characters
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }
    }
}
