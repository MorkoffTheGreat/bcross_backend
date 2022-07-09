using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using bcross_backend.DTOs;
using bcross_backend.Models;

namespace bcross_backend.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookController : Controller
    {
        [HttpGet("all")]
        public async Task<List<BookDTO>> GetAll()
        {
            using (var context = new bcrossContext())
                return await context.Books.Select(x => new BookDTO(x)).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<BookDTO> GetById(int id)
        {
            Book book;
            using (var context = new bcrossContext())
                book = await context.Books.FindAsync(id);
            return book is null ? null : new BookDTO(book);
        }

        [HttpGet("LastId")]
        public async Task<int> GetLastId()
        {
            Book book;
            using (var context = new bcrossContext())
                book = await context.Books.OrderBy(x => x.Id).LastOrDefaultAsync();
            return (book is null ? 0 : book.Id);
        }

        [HttpGet("getbyguid")]
        public async Task<BookDTO> GetByGuid(Guid guid)
        {
            Book book;
            using (var context = new bcrossContext())
                book = await context.Books.Where(x => x.Uuid == guid).FirstOrDefaultAsync();
            return (book is null ? null : new BookDTO(book));
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(BookDTO bookdto)
        {
            try
            {
                Book book = new Book()
                {
                    Id = bookdto.Id,
                    Name = bookdto.Name,
                    Author = bookdto.Author,
                    Year = bookdto.Year,
                    Uuid = bookdto.Uuid
                };

                using (var context = new bcrossContext())
                {
                    await context.AddAsync(book);
                    await context.SaveChangesAsync();
                }
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpPost("delete")]
        public async void Delete(int id)
        {
            try
            {
                using (var context = new bcrossContext())
                    context.Remove(await context.Books.FindAsync(id));
            }
            catch { }
        }

        [HttpGet("GetBookTravel")]
        public async Task<List<string>> GetBookTravel(Guid guid)
        {
            using (var context = new bcrossContext())
            {
                var book = await context.Books.Where(x => x.Uuid == guid).SingleOrDefaultAsync();
                return (book is null? null : await context.Bookrecords.Where(x => x.Book == book.Id).Select(t => t.Location).ToListAsync());
            }
        }
    }
}
