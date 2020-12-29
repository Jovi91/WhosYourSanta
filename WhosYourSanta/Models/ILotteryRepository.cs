using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public interface ILotteryRepository
    {
        public List<Lottery> GetLotteries(string IdUser);
        public Lottery Add(Lottery lottery);
    }
}
