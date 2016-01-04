using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// Represents link to a page containing an embedded video player or a video file
    /// </summary>
    public class InlineQueryResultVideo : InlineQueryResult
    {
        /// <summary>
        /// A valid URL for the embedded video player or video file
        /// </summary>
        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }
        /// <summary>
        /// Optional. Video width
        /// </summary>
        [JsonProperty("video_width")]
        public int VideoWidth { get; set; }
        /// <summary>
        /// Optional. Video height
        /// </summary>
        [JsonProperty("video_height")]
        public int VideoHeight { get; set; }
        /// <summary>
        /// Optional. Video duration in seconds
        /// </summary>
        [JsonProperty("video_duration")]
        public int VideoDuration { get; set; }
        /// <summary>
        /// Mime type of the content of video url, “text/html” or “video/mp4”
        /// </summary>
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
        /// <summary>
        /// URL of the thumbnail (jpeg only) for the video
        /// </summary>
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
