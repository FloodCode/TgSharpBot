using TgSharpBot.Types;
using TgSharpBot.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace TgSharpBot
{
    public class TelegramBot
    {
        private JsonSerializerSettings jsonSettings;
        public string Token { get; }
        public string ApiUrl { get; }
        public TelegramBot(string token)
        {
            Token = token;
            ApiUrl = "https://api.telegram.org/bot" + Token;
            jsonSettings = new JsonSerializerSettings();
            jsonSettings.ContractResolver = new TelegramResolver();
            jsonSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            jsonSettings.NullValueHandling = NullValueHandling.Include;
        }
        private TelegramType Deserialize<TelegramType>(string jsonObject)
        {
            Response<TelegramType> requestResponse = JsonConvert.DeserializeObject<Response<TelegramType>>(jsonObject);
            return requestResponse.Result;
        }
        public User GetMe()
        {
            string jsonResponse = Request.Send(ApiUrl + "/getMe");
            return jsonResponse == string.Empty ? null : Deserialize<User>(jsonResponse);
        }
        public Message SendMessage(int chatId, string text)
        {
            return SendMessage(chatId, text, null, false, null);
        }

        public Message SendMessage(int chatId, string text, int replyToMessageId)
        {
            return SendMessage(chatId, text, null, false, replyToMessageId);
        }

        public Message SendMessage(int chatId, string text, string parseMode = null,
            bool disableWebPagePreview = false, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("text", text);
            if (parseMode != null) parameters.Add("parse_mode", parseMode);
            if (disableWebPagePreview) parameters.Add("disable_web_page_preview", disableWebPagePreview);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            string jsonResponse = Request.Send(ApiUrl + "/sendMessage", parameters);
            return jsonResponse == string.Empty ? null : Deserialize<Message>(jsonResponse);
        }

        public Message SendMessage(string username, string text)
        {
            return SendMessage(username, text, null, false, null);
        }

        public Message SendMessage(string username, string text, int replyToMessageId)
        {
            return SendMessage(username, text, null, false, replyToMessageId);
        }

        public Message SendMessage(string username, string text, string parseMode = null,
            bool disableWebPagePreview = false, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", username);
            parameters.Add("text", text);
            if (parseMode != null) parameters.Add("parse_mode", parseMode);
            if (disableWebPagePreview) parameters.Add("disable_web_page_preview", disableWebPagePreview);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            string jsonResponse = Request.Send(ApiUrl + "/sendMessage", parameters);
            return jsonResponse == string.Empty ? null : Deserialize<Message>(jsonResponse);
        }

        public Message ForwardMessage(int chatId, int fromChatId, int messageId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("from_chat_id", fromChatId);
            parameters.Add("message_id", messageId);
            string jsonResponse = Request.Send(ApiUrl + "/forwardMessage", parameters);
            return jsonResponse == string.Empty ? null : Deserialize<Message>(jsonResponse);
        }

        public Message SendPhoto(int chatId, FileStream photo, string caption = null, int? replyToMessageId = null)
        {
            byte[] photoData = new byte[photo.Length];
            photo.Read(photoData, 0, photoData.Length);
            photo.Close();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("photo", new Request.FileParameter(photoData, "photo.jpg", "image/jpg"));
            if (caption != null) parameters.Add("caption", caption);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            string jsonResponse = Request.Send(ApiUrl + "/sendPhoto", parameters);
            return jsonResponse == string.Empty ? null : Deserialize<Message>(jsonResponse);
        }
    }
}
