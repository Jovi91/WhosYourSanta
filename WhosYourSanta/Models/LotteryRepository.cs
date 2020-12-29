﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public class LotteryRepository : ILotteryRepository
    {
        public AppDbContext Context { get; set; }

        public LotteryRepository(AppDbContext context)
        {
            Context = context;
        }
        public List<Lottery> GetLotteries(string idUser)
        {
            var user = Context.Users.Find(idUser);
            return Context.Lottery.Where(i => i.Admin == user).ToList();
        }

        public Lottery Add(Lottery lottery)
        {
            Context.Lottery.Add(lottery);
            Context.SaveChanges();
            return lottery;
        }
    }
}