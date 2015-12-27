using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TgSharpBot.Types
{
    public class Chat
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
