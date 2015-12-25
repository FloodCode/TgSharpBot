using Newtonsoft.Json.Serialization;
using System.Linq;

namespace TgSharpBot.Serialization
{
    public class TelegramResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return string.Concat(
                propertyName.Select(
                    (x, i) => i > 0 && char.IsUpper(x) ? "_" + char.ToLower(x).ToString() : x.ToString().ToLower()
                    )
                );
        }
    }
}
