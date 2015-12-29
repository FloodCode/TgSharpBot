using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    class ForceReply
    {
        [JsonProperty("force_reply")]
        public bool ForceReplyTrue { get { return true; } }
        public bool Selective { get; set; }
    }
}
