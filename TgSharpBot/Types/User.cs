using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// This class represents a Telegram user or bot
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for this user or bot
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// User‘s or bot’s first name
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Optional. User‘s or bot’s last name
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        /// <summary>
        /// Optional. User‘s or bot’s username
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
