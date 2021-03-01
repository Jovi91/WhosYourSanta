using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class AppUser:IdentityUser, IUser
    {
        //public string City { get; set; }
        public AppUser()
        {
            this.Santas = new HashSet<Santa>();
        }
        public ICollection<Santa> Santas { get; set; }

    }
}
