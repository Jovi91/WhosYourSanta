﻿using System;
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

        public async Task<IActionResult> Main(string idUser)
        {
            //Lottery lot = new Lottery()
            //{
            //    Name = "Crazy People",
            //    Admin = await UserManager.GetUserAsync(User),
            //    Santas = {new Santa()
            //    {
            //        Name="Bob",
            //        Email="aw@wp.pl"
            //   } }

            //};
            //List<Lottery> lotteries = new List<Lottery>();
            //lotteries.Add(lot);
            //MainViewModel model = new MainViewModel();
            //model.MyLotteries.Add(lot);
            //var user = await UserManager.FindByIdAsync(idUser);
            return View(LotteryRepository.GetLotteries(idUser));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
