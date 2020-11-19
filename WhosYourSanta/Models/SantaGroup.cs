using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class SantaGroup
    {
        public RegisteredUser Admin { get; set; }
        public List<Santa> Santas { get; set; }
    }
}
