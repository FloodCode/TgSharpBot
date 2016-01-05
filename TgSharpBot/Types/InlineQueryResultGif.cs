using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// Represents a link to an animated GIF file
    /// By default, this animated GIF file will be sent by the user with optional caption
    /// Alternatively, you can provide message_text to send it instead of the animation
    /// </summary>
    public class InlineQueryResultGif : InlineQueryResult
    {
        /// <summary>
        /// Don't change this option. Type of the result, must be gif
        /// </summary>
        [JsonProperty("type")]
        public override string Type
        {
            get
            {
                return "gif";
            }
        }
        /// <summary>
        /// A valid URL for the GIF file. File size must not exceed 1MB
        /// </summary>
        [JsonProperty("gif_url")]
        public string GifUrl { get; set; }
        /// <summary>
        /// Optional. Width of the GIF
        /// </summary>
        [JsonProperty("gif_width")]
        public int GifWidth { get; set; }
        /// <summary>
        /// Optional. Height of the GIF
        /// </summary>
        [JsonProperty("gif_height")]
        public int GifHeight { get; set; }
        /// <summary>
        /// URL of the static thumbnail for the result (jpeg or gif)
        /// </summary>
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
        /// <summary>
        /// Optional. Caption of the GIF file to be sent, 0-200 characters
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }
    }
}
