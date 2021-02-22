
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
        public ISantaRepository SantaRepository { get; }

        public LotteryRepository(AppDbContext context, ISantaRepository santaRepository)
        {
            Context = context;
            SantaRepository = santaRepository;
        }

        //public Dictionary<Santa,Lottery> GetSantaLotteryKeyValuePairs()
        //{
        //    Dictionary<Santa, Lottery> LotterySantaPairs = new Dictionary<Santa, Lottery>();

        //    foreach (var lottery in Context.Lottery.Include("Santas").Include("Admin"))
        //    {
        //        foreach (var santa in lottery.Santas)
        //        {
        //            LotterySantaPairs.Add(santa, lottery);
        //        }

        //    }
        //    return LotterySantaPairs;
        //}

        //public List<Lottery> GetUserLotteries(string idUser)
        //{
        //    // var user = Context.Users.Include("Santas").Where(u => u.Id == idUser).FirstOrDefault().Include("Lottery");
        //    var user = Context.Users.Find(idUser);
        //    //List<Lottery> lotteries = new List<Lottery>();

        //    //foreach (var santa in user.Santas)
        //    //{
        //    //    lotteries.Add(santa.Lottery);
        //    //}
        //    //return null;       
        //    //lotteries = user.Santas.Select((s) => s.Lottery).ToList();



        //    Dictionary<Santa, Lottery> dict = GetSantaLotteryKeyValuePairs();
        //    List<Lottery> memberLottery = dict.Where(kvp => user.Email == kvp.Key.Email).Select(kvp => kvp.Value).ToList();
        //    List<Lottery> adminLottery = Context.Lottery.Where(l => l.Admin == user).ToList();
        //    List<Lottery> allMyLotteries = memberLottery.Union(adminLottery).ToList();
        //    //
        //    //(item => item.Value.position.Equals(newPosition)).Select(item => item.Key);

        //    return allMyLotteries;
        //}

        public Lottery Add(Lottery lottery)
        {
            Context.Lottery.Add(lottery);
            Context.SaveChanges();
            return lottery;
        }
        public Lottery Delete(Lottery lottery)
        {
           lottery =  Context.Lottery.Include("Santas").Where(i => i.Id == lottery.Id).FirstOrDefault();

            //foreach(Santa santa in lottery.Santas)
            //{
            //    Context.Santas.Remove(santa);
            //}
            //Context.SaveChanges();
            //Context.Lottery.Remove(lottery);
            //Context.SaveChanges();

            lottery.Visibility = false;
            Context.SaveChanges();

            return lottery;

        }

        public Lottery GetLottery(int LotteryId)
        {
            //Lottery lottery = Context.Lottery.Find(id);
            Lottery lottery = Context.Lottery.Include("Santas").Include("Admin").Where(i => i.Id == LotteryId).FirstOrDefault();
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

        public List<Lottery> GetAllAdminsLotteries(string adminId)
        {
            return Context.Lottery.Where(l => l.Admin.Id == adminId).ToList();
        }

        public List<Santa> GetAllSantas(Lottery lottery)
        {
            throw new NotImplementedException();
        }
    }
}
