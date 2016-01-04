using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// Represents a link to a video animation (H.264/MPEG-4 AVC video without sound)
    /// By default, this animated MPEG-4 file will be sent by the user with optional caption
    /// Alternatively, you can provide message_text to send it instead of the animation
    /// </summary>
    public class InlineQueryResultMpeg4Gif : InlineQueryResult
    {
        /// <summary>
        /// A valid URL for the MP4 file. File size must not exceed 1MB
        /// </summary>
        [JsonProperty("mpeg4_url")]
        public string Mpeg4Url { get; set; }
        /// <summary>
        /// Optional. Video width
        /// </summary>
        [JsonProperty("mpeg4_width")]
        public int Mpeg4Width { get; set; }
        /// <summary>
        /// Optional. Video height
        /// </summary>
        [JsonProperty("mpeg4_height")]
        public int Mpeg4Height { get; set; }
        /// <summary>
        /// URL of the static thumbnail (jpeg or gif) for the result
        /// </summary>
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
        /// <summary>
        /// Optional. Caption of the MPEG-4 file to be sent, 0-200 characters
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }
    }
}
