using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TgSharpBot.Types
{
    class Response<ResponseType>
    {
        public bool Ok { get; set; }
        public ResponseType Result { get; set; }
    }
}
