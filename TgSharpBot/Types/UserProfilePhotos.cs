using System.Collections.Generic;

namespace TgSharpBot.Types
{
    public class UserProfilePhotos
    {
        public int TotalCount { get; set; }
        public List<PhotoSize[]> Photos { get; set; }
    }
}