using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public interface ILotteryRepository
    {
        public List<Lottery> GetUserLotteries(string IdUser);

        public Lottery GetLottery(int id);
        public Lottery Add(Lottery lottery);

        public Lottery Delete(Lottery lottery);
        public List<String> GetAllSantasFromAllLotteries();

        public bool ChcekIfLotteryHasStarted(int id);
    }
}
