﻿namespace TgSharpBot.Types
{
    public class Document
    {
        public string FileId { get; set; }
        public PhotoSize Thumb { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public int FileSize { get; set; }
    }
}
