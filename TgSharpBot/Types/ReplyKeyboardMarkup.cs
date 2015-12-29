﻿using System.Collections.Generic;
namespace TgSharpBot.Types
{
    class ReplyKeyboardMarkup
    {
        public List<List<string>> Keyboard { get; set; }
        public bool ResizeKeyboard { get; set; }
        public bool OneTimeKeyboard { get; set; }
        public bool Selective { get; set; }
    }
}
