using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// This class represents one result of an inline query
    /// </summary>
    public class InlineQueryResult
    {
        /// <summary>
        /// Type of the result, must be "article", "photo", "gif", "mpeg4_gif" or "video"
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Unique identifier for this result, 1-64 Bytes
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// Title of the result
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Text of the message to be sent
        /// </summary>
        [JsonProperty("message_text")]
        public string MessageText { get; set; }
        /// <summary>
        /// Optional. Send “Markdown”, if you want Telegram apps to show bold, italic and inline URLs in your bot's message.
        /// </summary>
        [JsonProperty("parse_mode")]
        public string ParseMode { get; set; }
        /// <summary>
        /// Optional. Disables link previews for links in the sent message
        /// </summary>
        [JsonProperty("disable_web_page_preview")]
        public bool DisableWebPagePreview { get; set; }
    }
}