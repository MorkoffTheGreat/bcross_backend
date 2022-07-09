using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using bcross_backend.Models;

namespace bcross_backend.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Rating { get; set; }

        public UserDTO() { }
        public UserDTO(User user)
        {
            this.Id = user.Id;
            this.Nickname = user.Nickname;
            this.Email = user.Email;
            this.City = user.City;
            this.Phone = user.Phone;
            this.Rating = user.Rating;
        }
    }
}
