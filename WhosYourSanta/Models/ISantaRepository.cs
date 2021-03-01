using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhosYourSanta.Models
{
    public interface ISantaRepository
    {
        IEnumerable<Santa> GetSantasFromLottery(int idLottery);
        Santa GetSanta(int Id);
        List<Santa> GetAllSantas(int lotteryId);
        List<Santa> GetSantasBy(string idUser);
        Santa GetSantaBy(string AppUserId, int lotteryId);
        Santa Add(Santa santa);
        Santa Update(Santa santaChanges);
        Santa Delete(int Id);
        List<Lottery> GetAllUsersLotteries(string idUser);
        AppUser GetAppUserByEmail(string Email);

        Santa DrawSantaForUser(string AppUserId, int lotteryId);
        Santa GetDrawnSanta(int santaWhoDrawsId);
       // bool UpdateSantasData(Santa santa);
    }
}
