using System.Collections.Generic;
using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// This class represent a user's profile pictures
    /// </summary>
    public class UserProfilePhotos
    {
        /// <summary>
        /// Total number of profile pictures the target user has
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        /// <summary>
        /// Requested profile pictures (in up to 4 sizes each)
        /// </summary>
        [JsonProperty("photos")]
        public List<PhotoSize[]> Photos { get; set; }
    }
}