using System;
using System.Collections.Generic;

namespace ChatApi.Models
{
    public partial class Chat
    {
        public int ChatId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string HashMessage { get; set; }
        public string? SaltMessage { get; set; }
        public DateTime? MessageTime { get; set; }

        public virtual UserInfo? Receiver { get; set; }
        public virtual UserInfo? Sender { get; set; }
    }
}
