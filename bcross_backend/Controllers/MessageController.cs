using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using bcross_backend.Models;
using bcross_backend.DTOs;

namespace bcross_backend.Controllers
{
    [ApiController]
    [Route("message")]
    public class MessageController : Controller
    {
        [HttpGet("all")]
        public async Task<List<MessageDTO>> GetAll()
        {
            using (var context = new bcrossContext())
                return await context.Messages.Select(x => new MessageDTO(x)).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<MessageDTO> GetById(int id)
        {
            Message message;
            using (var context = new bcrossContext())
                message = await context.Messages.FindAsync(id);
            return message is null ? null : new MessageDTO(message);
        }

        [HttpPost("add")]
        public async void Add(MessageDTO messagedto)
        {
            using (var context = new bcrossContext())
            {
                Message message = new Message()
                {
                    Id = messagedto.Id,
                    Sender = messagedto.Sender,
                    Receiver = messagedto.Receiver,
                    ReceiverNavigation = await context.Users.FindAsync(messagedto.Receiver), // чел не зануляется bruh
                    SenderNavigation = await context.Users.FindAsync(messagedto.Sender),
                    Textmessage = messagedto.Textmessage
                };
                await context.AddAsync(message);
                await context.SaveChangesAsync();
            }
        }

        [HttpPost("delete")]
        public async void Delete(int id)
        {
            using (var context = new bcrossContext())
                context.Remove(await context.Messages.FindAsync(id));
        }
    }
}
