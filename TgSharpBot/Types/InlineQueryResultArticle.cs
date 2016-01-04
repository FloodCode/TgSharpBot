using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// Represents a link to an article or web page
    /// </summary>
    public class InlineQueryResultArticle : InlineQueryResult
    {
        /// <summary>
        /// Optional. URL of the result
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
        /// <summary>
        /// Optional. Pass true, if you don't want the URL to be shown in the message
        /// </summary>
        [JsonProperty("hide_url")]
        public bool HideUrl { get; set; }
        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Optional. Url of the thumbnail for the result
        /// </summary>
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
        /// <summary>
        /// Optional. Thumbnail width
        /// </summary>
        [JsonProperty("thumb_width")]
        public int ThumbWidth { get; set; }
        /// <summary>
        /// Optional. Thumbnail height
        /// </summary>
        [JsonProperty("thumb_height")]
        public int ThumbHeight { get; set; }
    }
}
