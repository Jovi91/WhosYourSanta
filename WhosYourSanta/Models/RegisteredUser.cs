﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class RegisteredUser : IdentityUser<int>, IUser
    {
       // public List<Lottery> MyLotteries { get; set; }
    }
}
