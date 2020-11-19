using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class RegisteredUser : IdentityUser<int>, IUser
    {
        //public int Id { get; set; }
        //public string Email { get; set; }
        public string Password { get; set; }
    }
}
