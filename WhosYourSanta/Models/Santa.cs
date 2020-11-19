using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class Santa : IUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Santa DrawnSanta { get; set; }
    }
}
