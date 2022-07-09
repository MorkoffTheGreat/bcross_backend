using System;
using System.Collections.Generic;

#nullable disable

namespace bcross_backend.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string Textmessage { get; set; }

        public virtual User ReceiverNavigation { get; set; }
        public virtual User SenderNavigation { get; set; }
    }
}
