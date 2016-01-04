using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TgSharpBot.Types
{
    class InlineQueryResult
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string MessageText { get; set; }
        public string ParseMode { get; set; }
        public bool DisableWebPagePreview { get; set; }
    }
}