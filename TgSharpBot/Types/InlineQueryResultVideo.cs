using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TgSharpBot.Types
{
    class InlineQueryResultVideo : InlineQueryResult
    {
        public string VideoUrl { get; set; }
        public int VideoWidth { get; set; }
        public int VideoHeight { get; set; }
        public int VideoDuration { get; set; }
        public string MimeType { get; set; }
        public string ThumbUrl { get; set; }
        public string Description { get; set; }
    }
}
