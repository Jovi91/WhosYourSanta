
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public Lottery Delete(Lottery lottery)
        {
           lottery =  Context.Lottery.Include("Santas").Where(i => i.Id == lottery.Id).FirstOrDefault();

            foreach(Santa santa in lottery.Santas)
            {
                Context.Santas.Remove(santa);
            }
            Context.Lottery.Remove(lottery);
            Context.SaveChanges();
            return lottery;

        }

        public Lottery GetLottery(int id)
        {
            //Lottery lottery = Context.Lottery.Find(id);
            Lottery lottery = Context.Lottery.Include("Santas").Where(i => i.Id == id).FirstOrDefault();
            return lottery;
        }

        public List<string> GetAllSantasFromAllLotteries()
        {
            List<String> mylist= new List<string>();
           // Include("Santas") -> Eager Loading
            foreach (Lottery l in Context.Lottery.Include("Santas"))
            {
                foreach (Santa s in l.Santas)
                {
                    mylist.Add(l.Name + " " + s.Name);
                }
            }

            return mylist;
        }
        
        public bool ChcekIfLotteryHasStarted(int id)
        {
            Lottery lottery = this.GetLottery(id);
            List<Santa> santas = lottery.Santas.ToList();
            //bool lotteryStarted = santas.Where(s => s.DrawnSanta != null).Any();
            bool lotteryStarted = santas.Where(s => s.DrawnSanta != null).Any();
            return lotteryStarted;
        }
    }
}
