namespace TgSharpBot.Types
{
    class InlineQueryResultGif : InlineQueryResult
    {
        public string GifUrl { get; set; }
        public int GifWidth { get; set; }
        public int GifHeight { get; set; }
        public string ThumbUrl { get; set; }
        public string Caption { get; set; }
    }
}
