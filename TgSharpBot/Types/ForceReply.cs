using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// Upon receiving a message with object of this class, Telegram clients will display a reply interface to the user
    /// </summary>
    public class ForceReply
    {
        /// <summary>
        /// Shows reply interface to the user, as if they manually selected the bot‘s message and tapped "Reply"
        /// </summary>
        [JsonProperty("force_reply")]
        public bool ForceReplyTrue { get { return true; } }
        /// <summary>
        /// Optional. Use this parameter if you want to force reply from specific users only
        /// Targets:
        /// 1) users that are @mentioned in the text of the Message object;
        /// 2) if the bot's message is a reply (has reply_to_message_id), sender of the original message.
        /// </summary>
        [JsonProperty("selective")]
        public bool Selective { get; set; }
    }
}
