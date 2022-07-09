using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using bcross_backend.Models;

namespace bcross_backend.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public Guid Uuid { get; set; }

        public BookDTO() { }
        public BookDTO(Book book)
        {
            this.Id = book.Id;
            this.Name = book.Name;
            this.Author = book.Author;
            this.Year = book.Year;
            this.Uuid = book.Uuid;
        }
    }
}
