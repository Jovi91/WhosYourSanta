using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhosYourSanta.Models;

namespace WhosYourSanta.Controllers
{
    public class AdministrationController : Controller
    {
        public AdministrationController(ILotteryRepository lotteryRepository)
        {
            LotteryRepository = lotteryRepository;
        }

        public ILotteryRepository LotteryRepository { get; }

        public IActionResult DeleteLottery(int id)
        {
            Lottery lottery = LotteryRepository.GetLottery(id);
            if (lottery == null)
            {
                Response.StatusCode = 404;
                return View("LotteryNotFound", id);
            }
            LotteryRepository.Delete(lottery);

            return RedirectToAction("Main", "Home");
        }

        [HttpGet]
        public IActionResult PageInProgress()
        {
            return View();

        }

        [HttpGet]
        public IActionResult EditLotteryMember()
        {

            return View();

        }


    }
}
