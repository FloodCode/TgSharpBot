﻿using Newtonsoft.Json;

namespace TgSharpBot.Types
{
    /// <summary>
    /// This object represents a file ready to be downloaded
    /// The file can be downloaded via the link api.telegram.org/file/bot[token]/[file_path]
    /// It is guaranteed that the link will be valid for at least 1 hour
    /// When the link expires, a new one can be requested by calling GetFile()
    /// </summary>
    public class File
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        [JsonProperty("file_id")]
        public string FileId { get; set; }
        /// <summary>
        /// Optional. File size, if known
        /// </summary>
        [JsonProperty("file_size")]
        public int FileSize { get; set; }
        /// <summary>
        /// Optional. File path
        /// Use link api.telegram.org/file/bot[token]/[file_path] to get the file
        /// </summary>
        [JsonProperty("file_path")]
        public string FilePath { get; set; }
    }
}
