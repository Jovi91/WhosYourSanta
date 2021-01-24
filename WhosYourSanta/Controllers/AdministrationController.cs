using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosYourSanta.Models;

namespace WhosYourSanta.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        public AdministrationController(ILotteryRepository lotteryRepository, ISantaRepository santaRepository)
        {
            LotteryRepository = lotteryRepository;
            SantaRepository = santaRepository;
        }

        public ILotteryRepository LotteryRepository { get; }
        public ISantaRepository SantaRepository { get; }

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
        public IActionResult EditLotteryMember(int Id, int lotteryId)
        {
            Santa santa = SantaRepository.GetSanta(Id);
            bool lotteryStarted = LotteryRepository.ChcekIfLotteryHasStarted(lotteryId);
            ViewBag.lotteryStarted = lotteryStarted;
            ViewBag.lotteryId= lotteryId;

            return View(santa);
        }

        [HttpPost]
        public IActionResult EditLotteryMember(int lotteryId, int SantaId, Santa model)
        {
            if (ModelState.IsValid)
            {
                Santa updatedSanta = SantaRepository.GetSanta(SantaId);

              
                if(model.Name!=null)
                    updatedSanta.Name = model.Name;

                updatedSanta.Email = model.Email;
                SantaRepository.Update(updatedSanta);
            }
            //"details", new { id = newEmployee.Id })
            return RedirectToAction("LotteryDetails", "Home", new { id = lotteryId });
        }

    }
}
