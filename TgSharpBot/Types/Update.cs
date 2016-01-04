using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// This class represents an incoming update
    /// </summary>
    public class Update
    {
        /// <summary>
        /// The update‘s unique identifier
        /// </summary>
        [JsonProperty("update_id")]
        public int UpdateId { get; set; }
        /// <summary>
        /// Optional. New incoming message of any kind — text, photo, sticker, etc.
        /// </summary>
        [JsonProperty("message")]
        public Message Message { get; set; }
        /// <summary>
        /// Optional. New incoming inline query
        /// </summary>
        [JsonProperty("inline_query")]
        public InlineQuery InlineQuery { get; set; }
        /// <summary>
        /// Optional. The result of a inline query that was chosen by a user and sent to their chat partner
        /// </summary>
        [JsonProperty("chosen_inline_result")]
        public ChosenInlineResult ChosenInlineResult { get; set; }
    }
}
