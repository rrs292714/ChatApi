using System;
using System.Collections.Generic;

namespace ChatApi.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            ChatReceivers = new HashSet<Chat>();
            ChatSenders = new HashSet<Chat>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }

        public virtual ICollection<Chat> ChatReceivers { get; set; }
        public virtual ICollection<Chat> ChatSenders { get; set; }
    }
}
