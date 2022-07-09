using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using bcross_backend.Models;

namespace bcross_backend.DTOs
{
    public class BookrecordDTO
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Book { get; set; }
        public string Location { get; set; }
        public DateTime Sendtime { get; set; }
        public int? Receiver { get; set; }
        public DateTime? Receivetime { get; set; }
        public string Commentary { get; set; }

        public BookrecordDTO() { }

        public BookrecordDTO(Bookrecord record)
        {
            this.Id = record.Id;
            this.Sender = record.Sender;
            this.Book = record.Book;
            this.Location = record.Location;
            this.Sendtime = record.Sendtime;
            this.Receiver = record.Receiver;
            this.Receivetime = record.Receivetime;
            this.Commentary = record.Commentary;
        }
    }
}
