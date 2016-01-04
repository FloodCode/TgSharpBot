using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// Upon receiving a message with this object, Telegram clients will hide
    /// the current custom keyboard and display the default letter-keyboard
    /// </summary>
    public class ReplyKeyboardHide
    {
        /// <summary>
        /// Requests clients to hide the custom keyboard
        /// </summary>
        [JsonProperty("hide_keyboard")]
        public bool HideKeyboard {  get { return true; } }
        /// <summary>
        /// Optional. Use this parameter if you want to hide keyboard for specific users only
        /// Targets:
        /// 1) users that are @mentioned in the text of the Message object
        /// 2) if the bot's message is a reply (has reply_to_message_id), sender of the original message
        /// </summary>
        [JsonProperty("selective")]
        public bool Selective { get; set; }
    }
}
