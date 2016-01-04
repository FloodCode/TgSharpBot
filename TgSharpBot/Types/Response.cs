using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// Represents bot API response with Ok status
    /// </summary>
    /// <typeparam name="ResponseType"></typeparam>
    class Response<ResponseType>
    {
        /// <summary>
        /// Status of response
        /// </summary>
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        /// <summary>
        /// Telegram object, result of request
        /// </summary>
        [JsonProperty("result")]
        public ResponseType Result { get; set; }
    }
}
