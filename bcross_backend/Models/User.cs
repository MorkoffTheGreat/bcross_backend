using System;
using System.Collections.Generic;

#nullable disable

namespace bcross_backend.Models
{
    public partial class User
    {
        public User()
        {
            BookrecordReceiverNavigations = new HashSet<Bookrecord>();
            BookrecordSenderNavigations = new HashSet<Bookrecord>();
            MessageReceiverNavigations = new HashSet<Message>();
            MessageSenderNavigations = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Rating { get; set; }

        public virtual ICollection<Bookrecord> BookrecordReceiverNavigations { get; set; }
        public virtual ICollection<Bookrecord> BookrecordSenderNavigations { get; set; }
        public virtual ICollection<Message> MessageReceiverNavigations { get; set; }
        public virtual ICollection<Message> MessageSenderNavigations { get; set; }
    }
}
