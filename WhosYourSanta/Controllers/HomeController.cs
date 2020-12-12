using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WhosYourSanta.Models;
using WhosYourSanta.ViewModel;

namespace WhosYourSanta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public UserManager<IdentityUser> UserManager { get; }
        public ILotteryRepository LotteryRepository { get; }

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ILotteryRepository lotteryRepository)
        {
            _logger = logger;
            UserManager = userManager;
            LotteryRepository = lotteryRepository;
        }



        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Main()
        {
            var userId = UserManager.GetUserId(User);

            ; return View(LotteryRepository.GetLotteries(userId));
        }

        public IActionResult AddLottery()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
