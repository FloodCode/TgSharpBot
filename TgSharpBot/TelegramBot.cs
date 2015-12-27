using TgSharpBot.Types;
using TgSharpBot.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

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
        /// <summary>
        /// Identifier of the last accepted update
        /// </summary>
        public int LastUpdateId { get { return _lastUpdateId; } }
        
        /// <param name="token">Unique authentication token</param>
        public TelegramBot(string token)
        {
            Token = token;
            ApiUrl = "https://api.telegram.org/bot" + Token + "/";
            User botInfo = GetMe();
            if (botInfo == null)
            {
                throw new ArgumentNullException("botInfo", "Can't get basic information about the bot. Your token must be valid.");
            }
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

        /// <summary>
        /// Method for testing your bot's auth token
        /// </summary>
        /// <returns>Basic information about the bot in form of a User object</returns>
        public User GetMe()
        {
            string jsonResponse = Request.Send(ApiUrl + "getMe");
            return jsonResponse == string.Empty ? null : Deserialize<User>(jsonResponse).Result;
        }
        
        /// <summary>
        /// Gets new updates
        /// </summary>
        /// <returns>List of new updates</returns>
        public List<Update> GetUpdates()
        {
            return GetUpdates(_lastUpdateId + 1);
        }

        /// <summary>
        /// Gets new updates
        /// </summary>
        /// <param name="offset">Identifier of the first update to be returned. Must be greater by one than the highest among the identifiers of previously received updates</param>
        /// <param name="limit">Limits the number of updates to be retrieved. Values between 1—100 are accepted</param>
        /// <param name="timeout">Timeout in seconds for long polling</param>
        /// <returns>List of new updates</returns>
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

        /// <summary>
        /// Sends text message to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <returns>Sent message</returns>
        public Message SendMessage(long chatId, string text)
        {
            return SendMessage(chatId, text, null, false, null);
        }

        /// <summary>
        /// Sends text message to chat 
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendMessage(long chatId, string text, int replyToMessageId)
        {
            return SendMessage(chatId, text, null, false, replyToMessageId);
        }

        /// <summary>
        /// Sends text message to chat 
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="parseMode">Send "Markdown" for parse mode</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendMessage(long chatId, string text, string parseMode = null,
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

        /// <summary>
        /// Sends text message to chat 
        /// </summary>
        /// <param name="username">Username of the target channel (in the format @channelusername)</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <returns>Sent message</returns>
        public Message SendMessage(string username, string text)
        {
            return SendMessage(username, text, null, false, null);
        }

        /// <summary>
        /// Sends text message to chat
        /// </summary>
        /// <param name="username">Username of the target channel (in the format @channelusername)</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendMessage(string username, string text, int replyToMessageId)
        {
            return SendMessage(username, text, null, false, replyToMessageId);
        }

        /// <summary>
        /// Sends text message to chat
        /// </summary>
        /// <param name="username">Username of the target channel (in the format @channelusername)</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="parseMode">Send "Markdown" for parse mode</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
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

        /// <summary>
        /// Forwards message of any kind
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="fromChatId">Unique identifier for the chat where the original message was sent</param>
        /// <param name="messageId">Unique message identifier</param>
        /// <returns>Sent message</returns>
        public Message ForwardMessage(long chatId, int fromChatId, int messageId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("from_chat_id", fromChatId);
            parameters.Add("message_id", messageId);
            return ProcessMethod("forwardMessage", parameters);
        }

        /// <summary>
        /// Sends photo to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="fileId">Unique identifier of photo</param>
        /// <param name="caption">Photo caption</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendPhoto(long chatId, string fileId, string caption = null, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("photo", fileId);
            if (caption != null) parameters.Add("caption", caption);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendPhoto", parameters);
        }

        /// <summary>
        /// Sends photo to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="photo">File stream of photo</param>
        /// <param name="caption">Photo caption</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendPhoto(long chatId, FileStream photo, string caption = null, int? replyToMessageId = null)
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

        /// <summary>
        /// Sends photo to chat
        /// </summary>
        /// <param name="username">Username of the target channel (in the format @channelusername)</param>
        /// <param name="fileId">Unique identifier of photo</param>
        /// <param name="caption">Photo caption</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendPhoto(string username, string fileId, string caption = null, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", username);
            parameters.Add("photo", fileId);
            if (caption != null) parameters.Add("caption", caption);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendPhoto", parameters);
        }

        /// <summary>
        /// Sends photo to chat
        /// </summary>
        /// <param name="username">Username of the target channel (in the format @channelusername)</param>
        /// <param name="photo">File stream of photo</param>
        /// <param name="caption">Photo caption</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendPhoto(string username, FileStream photo, string caption = null, int? replyToMessageId = null)
        {
            byte[] photoData = new byte[photo.Length];
            photo.Read(photoData, 0, photoData.Length);
            photo.Close();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", username);
            parameters.Add("photo", new Request.FileParameter(photoData, "photo.jpg", "image/jpg"));
            if (caption != null) parameters.Add("caption", caption);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendPhoto", parameters);
        }

        /// <summary>
        /// Sends audio to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="fileId">Unique identifier of audio</param>
        /// <param name="duration">Duration of the audio in seconds</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendAudio(long chatId, string fileId, int? duration = null, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("audio", fileId);
            if (duration != null) parameters.Add("duration", duration);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendAudio", parameters);
        }

        /// <summary>
        /// Sends audio to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="audio">File stream of audio</param>
        /// <param name="duration">Duration of the audio in seconds</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendAudio(long chatId, FileStream audio, int? duration = null, int? replyToMessageId = null)
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

        /// <summary>
        /// Sends document to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="fileId">Unique identifier of document</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendDocument(long chatId, string fileId, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("document", fileId);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendDocument", parameters);
        }

        /// <summary>
        /// Sends document to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="document">File stream of document</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendDocument(long chatId, FileStream document, int? replyToMessageId = null)
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

        /// <summary>
        /// Sends sticker to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="fileId">Unique identifier of sticker</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendSticker(long chatId, string fileId, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("sticker", fileId);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendSticker", parameters);
        }

        /// <summary>
        /// Sends sticker to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="sticker">File stream of sticker</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendSticker(long chatId, FileStream sticker, int? replyToMessageId = null)
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

        /// <summary>
        /// Sends video to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="fileId">Unique identifier of video</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendVideo(long chatId, string fileId, int? duration = null, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("video", fileId);
            if (duration != null) parameters.Add("duration", duration);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendVideo", parameters);
        }

        /// <summary>
        /// Sends video to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="video">File stream of video</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendVideo(long chatId, FileStream video, int? duration = null, int? replyToMessageId = null)
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

        /// <summary>
        /// Sends voice to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="fileId">Unique identifier of voice</param>
        /// <param name="duration">Duration of sent audio in seconds</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendVoice(long chatId, int fileId, int? duration = null, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("voice", fileId);
            if (duration != null) parameters.Add("duration", duration);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendVoice", parameters);
        }

        /// <summary>
        /// Sends voice to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="voice">File stream of voice</param>
        /// <param name="duration">Duration of sent audio in seconds</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendVoice(long chatId, FileStream voice, int? duration = null, int? replyToMessageId = null)
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

        /// <summary>
        /// Sends location to chat
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <returns>Sent message</returns>
        public Message SendLocation(long chatId, float latitude, float longitude, int? replyToMessageId = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("latitude", latitude);
            parameters.Add("longitude", longitude);
            if (replyToMessageId != null) parameters.Add("reply_to_message_id", replyToMessageId);
            return ProcessMethod("sendLocation", parameters);
        }

        /// <summary>
        /// Sends chat action
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="chatAction">Type of action to broadcast</param>
        /// <returns>Returns true if success</returns>
        public bool SendChatAction(long chatId, string chatAction)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("chat_id", chatId);
            parameters.Add("action", chatAction);
            string jsonResponse = Request.Send(ApiUrl + "sendChatAction", parameters);
            Response<string> response = Deserialize<string>(jsonResponse);
            return response == null ? false : response.Ok;
        }

        /// <summary>
        /// Gets user profile photos
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="offset">Sequential number of the first photo to be returned</param>
        /// <param name="limit">Limits the number of photos to be retrieved. Values between 1—100 are accepted</param>
        /// <returns>User profile photos</returns>
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

        /// <summary>
        /// Gets file info
        /// </summary>
        /// <param name="fileId">File identifier to get info about</param>
        /// <returns>File info</returns>
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