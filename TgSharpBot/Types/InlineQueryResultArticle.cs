namespace TgSharpBot.Types
{
    class InlineQueryResultArticle : InlineQueryResult
    {
        public string Url { get; set; }
        public bool HideUrl { get; set; }
        public string Description { get; set; }
        public string ThumbUrl { get; set; }
        public int ThumbWidth { get; set; }
        public int ThumbHeight { get; set; }
    }
}
