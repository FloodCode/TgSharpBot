using System;

namespace TgSharpBot.Types
{
    public class Message
    {
        public int MessageId { get; set; }
        public User From { get; set; }
        public int Date { get; set; }
        public Chat Chat { get; set; }
        public User ForwardFrom { get; set; }
        public int ForwardDate { get; set; }
        public string Text { get; set; }
        public Audio Audio { get; set; }
        public Document Document { get; set; }
        public PhotoSize[] Photo { get; set; }
        public Sticker Sticker { get; set; }
        public Video Video { get; set; }
        public Contact Contact { get; set; }
        public Location Location { get; set; }
        public User NewChatParticipant { get; set; }
        public User LeftChatParticipant { get; set; }
        public string NewChatTitle { get; set; }
        public PhotoSize[] NewChatPhoto { get; set; }
        public bool DeleteChatPhoto { get; set; }
        public bool GroupChatCreated { get; set; }
    }
}
