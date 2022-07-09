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
    [Route("bookrecord")]
    public class BookrecordController : Controller
    {
        [HttpGet("all")]
        public async Task<List<BookrecordDTO>> GetAll()
        {
            using (var context = new bcrossContext())
                return await context.Bookrecords.Select(x => new BookrecordDTO(x)).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<BookrecordDTO> GetById(int id)
        {
            Bookrecord bookrecord;
            using (var context = new bcrossContext())
                bookrecord = await context.Bookrecords.FindAsync(id);
            return bookrecord is null ? null : new BookrecordDTO(bookrecord);
        }

        [HttpPost("add")]
        public async void Add(BookrecordDTO bookrecorddto)
        {
            using (var context = new bcrossContext())
            {
                Bookrecord bookrecord = new Bookrecord()
                {
                    Id = bookrecorddto.Id,
                    Book = bookrecorddto.Book,
                    Sender = bookrecorddto.Sender,
                    Location = bookrecorddto.Location,
                    Sendtime = bookrecorddto.Sendtime,
                    Receiver = bookrecorddto.Receiver,
                    Receivetime = bookrecorddto.Receivetime,
                    Commentary = bookrecorddto.Commentary,
                    BookNavigation = await context.Books.FindAsync(bookrecorddto.Book),
                    ReceiverNavigation = context.Users.Find(bookrecorddto.Receiver), // чел не зануляется bruh
                    SenderNavigation = await context.Users.FindAsync(bookrecorddto.Sender),
                };
                await context.AddAsync(bookrecord);
                await context.SaveChangesAsync();
            }
        }

        [HttpPost("addnodto")]
        public async void AddNoDto(Bookrecord bookrecord)
        {
            using (var context = new bcrossContext())
            {
                await context.AddAsync(bookrecord);
                await context.SaveChangesAsync();
            }
        }

        [HttpPost("put")]
        public async Task<ActionResult<Guid>> PutBook(int bookid, int senderid, string location, DateTime sendtime, string commentary)
        {
            try
            {
                using (var context = new bcrossContext())
                {
                    Bookrecord bookrecord = new Bookrecord()
                    {
                        Book = bookid,
                        Sender = senderid,
                        Location = location,
                        Sendtime = sendtime,
                        Commentary = commentary
                    };
                    var book = await context.Books.FindAsync(bookid);
                    if (book is null)
                        return BadRequest();
                    var sender = await context.Users.FindAsync(senderid);
                    if (sender is null)
                        return BadRequest();
                    bookrecord.BookNavigation = book;
                    bookrecord.SenderNavigation = sender;
                    context.AttachRange(bookrecord.BookNavigation, bookrecord.SenderNavigation);
                    await context.AddAsync(bookrecord);
                    await context.SaveChangesAsync();
                    return book.Uuid;
                }
            }
            catch { return BadRequest(); }
        }

        [HttpPost("take")]
        public async Task<ActionResult> TakeBook(Guid guid, int receiver, DateTime receivetime)
        {
            using (var context = new bcrossContext())
            {
                Bookrecord bookrecord = await context.Bookrecords.Where(x => x.BookNavigation.Uuid == guid).OrderBy(x => x.Id).LastOrDefaultAsync();
                if (bookrecord is null)
                    return BadRequest("No available book with this GUID");
                bookrecord.Receiver = receiver;
                bookrecord.Receivetime = receivetime;
                var receiverA = await context.Users.FindAsync(receiver);
                if (receiverA is null)
                    return BadRequest("User error, try relogging");
                bookrecord.ReceiverNavigation = receiverA;
                context.Attach(receiverA);
                var senderA = await context.Users.FindAsync(bookrecord.Sender);
                senderA.Rating++;
                context.Attach(senderA);
                await context.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpGet("sentbooks")]
        public async Task<List<BookDTO>> GetSentBooks(int userid)
        {
            using (var context = new bcrossContext())
            {
                var records = await context.Bookrecords.Include("BookNavigation").Where(x => x.Sender == userid).ToListAsync();
                return records.Select(x => new BookDTO(x.BookNavigation)).ToList();
            }
        }

        [HttpGet("receivedbooks")]
        public async Task<List<BookDTO>> GetReceivedBooks(int userid)
        {
            using (var context = new bcrossContext())
            {
                var records = await context.Bookrecords.Include("BookNavigation").Where(x => x.Receiver == userid).ToListAsync();
                return records.Select(x => new BookDTO(x.BookNavigation)).ToList();
            }
        }

        [HttpPost("delete")]
        public async void Delete(int id)
        {
            try
            {
                using (var context = new bcrossContext())
                {
                    context.Remove(await context.Bookrecords.FindAsync(id));
                    await context.SaveChangesAsync();
                }
            }
            catch { }
        }

        [HttpGet("GetAllPutBooksLocations")]
        public async Task<List<BookrecordDTO>> GetAllPutBooks()
        {
            using (var context = new bcrossContext())
                return await context.Bookrecords.Where(x => x.Receiver == null).Select(x => new BookrecordDTO(x)).ToListAsync();
        }
    }
}
