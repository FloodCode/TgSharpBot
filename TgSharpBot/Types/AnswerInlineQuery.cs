using System.Collections.Generic;

namespace TgSharpBot.Types
{
    class AnswerInlineQuery
    {
        public string InlineQueryId { get; set; }
        public List<InlineQueryResult> Results { get; set; }
        public int CacheTime { get; set; }
        public bool IsPersonal { get; set; }
        public string NextOffset { get; set; }
    }
}
