using System;
using System.Collections.Generic;

#nullable disable

namespace bcross_backend.Models
{
    public partial class Bookrecord
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Book { get; set; }
        public string Location { get; set; }
        public DateTime Sendtime { get; set; }
        public int? Receiver { get; set; }
        public DateTime? Receivetime { get; set; }
        public string Commentary { get; set; }

        public virtual Book BookNavigation { get; set; }
        public virtual User ReceiverNavigation { get; set; }
        public virtual User SenderNavigation { get; set; }
    }
}
