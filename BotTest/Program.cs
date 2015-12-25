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
            TelegramBot bot = new TelegramBot("105957964:AAEP_s6E-3Aj32uI7lxjnxis6rlS5lGkjDE");
            Message msg = bot.SendPhoto(80667864, new FileStream(System.IO.Path.Combine(@"D:\RainbowM\", "2.jpg"), FileMode.Open));
            Console.WriteLine(msg.Photo.Last().FileId);
            Console.ReadKey();
        }
    }
}
