using TgSharpBot.Types;
using TgSharpBot.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TgSharpBot
{
    public class TelegramBot
    {
        private JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            ContractResolver = new TelegramResolver(),
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Include
        };
        private int _lastUpdateId = 0;
        public string Token { get; }
        public string ApiUrl { get; }
        public int LastUpdateId { get { return _lastUpdateId; } }
        public TelegramBot(string token)
        {
            Token = token;
            ApiUrl = "https://api.telegram.org/bot" + Token + "/";
        }

        private Response<TelegramType> Deserialize<TelegramType>(string jsonObject)
        {
            Response<TelegramType> requestResponse = JsonConvert.DeserializeObject<Response<TelegramType>>(jsonObject, jsonSettings);
            return requestResponse;
        }

        private Message ProcessMethod(string methodName, Dictionary<string, object> parameters)
        {
            string jsonResponse = Request.Send(ApiUrl + methodName, parameters);
            if (jsonResponse != null)
            {
                return Deserialize<Message>(jsonResponse).Result;
            }
            else
            {
                return null;
            }
        }

        public User GetMe()
        {
            string jsonResponse = Request.Send(ApiUrl + "getMe");
            return jsonResponse == string.Empty ? null : Deserialize<User>(jsonResponse).Result;
        }

        public List<Update> GetUpdates()
        {
            return GetUpdates(_lastUpdateId + 1);
        }

        public List<Update> GetUpdates(int? offset = null, int? limit = null, int? timeout = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (offset != null) parameters.Add("offset", offset);
            if (limit != null) parameters.Add("limit", limit);
            if (timeout != null) parameters.Add("timeout", timeout);
            string jsonResponse = Request.Send(ApiUrl + "getUpdates", parameters);
            Response<List<Update>> response = Deserialize<List<Update>>(jsonResponse);
            if (response != null)
            {
                if (response.Ok)
                {
                    if (response.Result.Count > 0)
                    {
                        _lastUpdateId = response.Result.Last().UpdateId;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return response.Result;
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
            return ProcessMethod("sendMessage", parameters);
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
            return ProcessMethod("sendMessage", parameters);
        }

        public Message ForwardMessage(int chatId, int fromChatId, int messageId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("from_chat_id", fromChatId);
            parameters.Add("message_id", messageId);
            return ProcessMethod("forwardMessage", parameters);
        }

        public Message SendPhoto(int chatId, string fileId, string caption = null, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("photo", fileId);
            if (caption != null) parameters.Add("caption", caption);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendPhoto", parameters);
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
            return ProcessMethod("sendPhoto", parameters);
        }

        public Message SendAudio(int chatId, string fileId, int? duration = null, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("audio", fileId);
            if (duration != null) parameters.Add("duration", duration);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendAudio", parameters);
        }

        public Message SendAudio(int chatId, FileStream audio, int? duration = null, int? replyToMessageId = null)
        {
            byte[] audioData = new byte[audio.Length];
            audio.Read(audioData, 0, audioData.Length);
            audio.Close();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("audio", new Request.FileParameter(audioData, "audio.ogg", "audio/ogg"));
            if (duration != null) parameters.Add("duration", duration);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendAudio", parameters);
        }

        public Message SendDocument(int chatId, string fileId, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("document", fileId);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendDocument", parameters);
        }

        public Message SendDocument(int chatId, FileStream document, int? replyToMessageId = null)
        {
            byte[] documentData = new byte[document.Length];
            document.Read(documentData, 0, documentData.Length);
            document.Close();
            string documentName = Path.GetFileName(document.Name);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("document", new Request.FileParameter(documentData, documentName, "application/octet-stream"));
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendDocument", parameters);
        }

        public Message SendSticker(int chatId, string fileId, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("sticker", fileId);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendSticker", parameters);
        }

        public Message SendSticker(int chatId, FileStream sticker, int? replyToMessageId = null)
        {
            byte[] stickerData = new byte[sticker.Length];
            sticker.Read(stickerData, 0, stickerData.Length);
            sticker.Close();
            string stickerName = Path.GetFileName(sticker.Name);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("sticker", new Request.FileParameter(stickerData, stickerName, "image/webp"));
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendSticker", parameters);
        }

        public Message SendVideo(int chatId, string fileId, int? duration = null, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("video", fileId);
            if (duration != null) parameters.Add("duration", duration);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendVideo", parameters);
        }

        public Message SendVideo(int chatId, FileStream video, int? duration = null, int? replyToMessageId = null)
        {
            byte[] videoData = new byte[video.Length];
            video.Read(videoData, 0, videoData.Length);
            video.Close();
            string videoName = Path.GetFileName(video.Name);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("video", new Request.FileParameter(videoData, videoName, "video/mp4"));
            if (duration != null) parameters.Add("duration", duration);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendVideo", parameters);
        }

        public Message SendVoice(int chatId, int fileId, int? duration = null, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("voice", fileId);
            if (duration != null) parameters.Add("duration", duration);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendVoice", parameters);
        }

        public Message SendVoice(int chatId, FileStream voice, int? duration = null, int? replyToMessageId = null)
        {
            byte[] voiceData = new byte[voice.Length];
            voice.Read(voiceData, 0, voiceData.Length);
            voice.Close();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("voice", new Request.FileParameter(voiceData, "voice.ogg", "audio/ogg"));
            if (duration != null) parameters.Add("duration", duration);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendVoice", parameters);
        }

        public Message SendLocation(int chatId, float latitude, float longitude, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("latitude", latitude);
            parameters.Add("longitude", longitude);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendLocation", parameters);
        }

        public bool SendChatAction(int chatId, string chatAction)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("action", chatAction);
            string jsonResponse = Request.Send(ApiUrl + "sendChatAction", parameters);
            Response<string> response = Deserialize<string>(jsonResponse);
            return response == null ? false : response.Ok;
        }

        public UserProfilePhotos GetUserProfilePhotos(int userId, int? offset = null, int? limit = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("user_id", userId);
            if (offset != null) parameters.Add("offset", offset);
            if (limit != null) parameters.Add("limit", limit);
            string jsonResponse = Request.Send(ApiUrl + "getUserProfilePhotos", parameters);
            Response<UserProfilePhotos> response = Deserialize<UserProfilePhotos>(jsonResponse);
            return response == null ? null : response.Ok ? response.Result : null;
        }

        public TgSharpBot.Types.File GetFile(string fileId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("file_id", fileId);
            string jsonResponse = Request.Send(ApiUrl + "getFile", parameters);
            Response<TgSharpBot.Types.File> response = Deserialize<TgSharpBot.Types.File>(jsonResponse);
            return response == null ? null : response.Ok ? response.Result : null;
        }
    }
}
