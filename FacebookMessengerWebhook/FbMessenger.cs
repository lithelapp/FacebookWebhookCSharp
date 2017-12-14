using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FacebookMessengerWebhook
{
    [DataContract]
    public class WebhookMessageRequest
    {
        [Required]
        [DataMember(Name = "object")]
        public string Object { get; set; }
        [Required]
        [DataMember(Name = "entry")]
        public List<Entry> Entries { get; set; }
    }

    [DataContract]
    public class Entry
    {
        [Required]
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [Required]
        [DataMember(Name = "time")]
        public long Time { get; set; }
        [Required]
        [DataMember(Name = "messaging")]
        public List<Messaging> Messagings { get; set; }
    }

    public class Messaging
    {
        [Required]
        public User Sender { get; set; }
        [Required]
        public User Recipient { get; set; }
        public long Timestamp { get; set; }
        public MessageContent Message { get; set; }

        public class User
        {
            [Required]
            public string Id { get; set; }
        }

        [DataContract]
        public class MessageContent
        {
            [Required]
            [DataMember(Name = "mid")]
            public string Mid { get; set; }
            [DataMember(Name = "text")]
            public string Text { get; set; }
            [DataMember(Name = "quick_reply")]
            public QuckyReplyContent QuickReply { get; set; }
            public class QuckyReplyContent
            {
                public string Payload { get; set; }
            }
        }
    }

    [DataContract]
    public class SendRequest
    {
        [DataMember(Name = "recipient")]
        public RecipientObject Recipient { get; set; }
        [DataMember(Name = "message")]
        public MessageContent Message { get; set; }

        [DataContract]
        public class RecipientObject
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }
            [DataMember(Name = "phone_number")]
            public string PhoneNumber { get; set; }
            //[DataMember(Name = "name")]
            //public NameObject Name { get; set; }
        }

        [DataContract]
        public class MessageContent
        {
            [DataMember(Name = "text")]
            public string Text { get; set; }
        }
    }
}
