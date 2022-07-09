using System;
using System.Collections.Generic;

#nullable disable

namespace bcross_backend.Models
{
    public partial class Book
    {
        public Book()
        {
            Bookrecords = new HashSet<Bookrecord>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public Guid Uuid { get; set; }

        public virtual ICollection<Bookrecord> Bookrecords { get; set; }
    }
}
