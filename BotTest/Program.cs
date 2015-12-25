using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TgSharpBot;
using TgSharpBot.Types;

namespace BotTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TelegramBot bot = new TelegramBot("[YOUR TOKEN HERE]");
            Console.ReadKey();
        }
    }
}
