using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// This class represents a result of an inline query that was chosen by the user and sent to their chat partner
    /// </summary>
    public class ChosenInlineResult
    {
        /// <summary>
        /// The unique identifier for the result that was chosen
        /// </summary>
        [JsonProperty("result_id")]
        public string ResultId { get; set; }
        /// <summary>
        /// The user that chose the result
        /// </summary>
        [JsonProperty("from")]
        public User From { get; set; }
        /// <summary>
        /// The query that was used to obtain the result
        /// </summary>
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}
