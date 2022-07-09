using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using bcross_backend.Models;

namespace bcross_backend.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string Textmessage { get; set; }

        public MessageDTO() { }
        public MessageDTO(Message message)
        {
            this.Id = message.Id;
            this.Sender = message.Sender;
            this.Receiver = message.Receiver;
            this.Textmessage = message.Textmessage;
        }
    }
}
