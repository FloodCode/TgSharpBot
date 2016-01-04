namespace TgSharpBot.Types
{
    class InlineQueryResultMpeg4Gif : InlineQueryResult
    {
        public string Mpeg4Url { get; set; }
        public int Mpeg4Width { get; set; }
        public int Mpeg4Height { get; set; }
        public string ThumbUrl { get; set; }
        public string Caption { get; set; }
    }
}
