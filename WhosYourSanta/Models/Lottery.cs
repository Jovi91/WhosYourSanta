﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class Lottery
    {
        [Required]
        public string Name { get; set; }
        public IdentityUser Admin { get; set; }
        public List<Santa> Santas { get; set; }
    }
}
