namespace TgSharpBot.Types
{
    class InlineQueryResultPhoto : InlineQueryResult
    {
        public string PhotoUrl { get; set; }
        public string MimeType { get; set; }
        public int PhotoWidth { get; set; }
        public int PhotoHeight { get; set; }
        public string ThumbUrl { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
    }
}
