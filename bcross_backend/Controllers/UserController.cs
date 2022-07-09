using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

using bcross_backend.Models;
using bcross_backend.DTOs;


namespace bcross_backend.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        [HttpGet("all")]
        public async Task<List<UserDTO>> GetAll()
        {
            using (var context = new bcrossContext())
                return await context.Users.Select(x => new UserDTO(x)).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<UserDTO> GetById(int id)
        {
            User user;
            using (var context = new bcrossContext())
                user = await context.Users.FindAsync(id);
            return user is null ? null : new UserDTO(user);
        }

        [HttpGet("find")]
        public async Task<UserDTO> GetByNickname(string nickname)
        {
            User user;
            using (var context = new bcrossContext())
                user = await context.Users.Where(x => x.Nickname == nickname).SingleOrDefaultAsync();
            return user is null ? null : new UserDTO(user);
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(UserDTO userdto)
        {
            try
            {
                User user = new User()
                {
                    Nickname = userdto.Nickname,
                    Email = userdto.Email,
                    City = userdto.City,
                    Phone = userdto.Phone,
                    Rating = 0
                };

                using (var context = new bcrossContext())
                {
                    await context.AddAsync(user);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch { return BadRequest(); }
        }

        [HttpPost("delete")]
        public async void Delete(int id)
        {
            using (var context = new bcrossContext())
            {
                try
                {
                    context.Remove(await context.Users.FindAsync(id));
                    await context.SaveChangesAsync();
                }
                catch { }
            }
        }

        [HttpGet("idbyemail")]
        public async Task<int?> GetUserIdByEmail(string email)
        {
            using (var context = new bcrossContext())
            {
                var user = await context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                return (user is null ? null : user.Id);
            }
        }

        [HttpGet("scoreboard")]
        public async Task<List<User>> GetScoreboard()
        {
            using (var context = new bcrossContext())
                return await context.Users.OrderByDescending(x => x.Rating).Take(20).ToListAsync();
        }
    }
}
