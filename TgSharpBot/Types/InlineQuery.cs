using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// This class represents an incoming inline query
    /// When the user sends an empty query, your bot could return some default or trending results
    /// </summary>
    class InlineQuery
    {
        /// <summary>
        /// Unique identifier for this query
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty("from")]
        public User From { get; set; }
        /// <summary>
        /// Text of the query
        /// </summary>
        [JsonProperty("query")]
        public string Query { get; set; }
        /// <summary>
        /// Offset of the results to be returned, can be controlled by the bot
        /// </summary>
        [JsonProperty("offset")]
        public string Offset { get; set; }
    }
}
