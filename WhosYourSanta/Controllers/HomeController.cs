using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using PasswordGenerator;
using WhosYourSanta.Models;


namespace WhosYourSanta.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        public UserManager<AppUser> UserManager { get; }
        public ILotteryRepository LotteryRepository { get; }

        public SignInManager<AppUser> SignInManager { get; }

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, ILotteryRepository lotteryRepository, SignInManager<AppUser> signInManager)
        {
            Logger = logger;
            UserManager = userManager;
            LotteryRepository = lotteryRepository;
            SignInManager = signInManager;
        }



        [AllowAnonymous]
        public IActionResult Index()
        {
            if (SignInManager.IsSignedIn(User))
                return RedirectToAction("Main");
            else
                return View();
        }

        public IActionResult Main()
        {
            var userId = UserManager.GetUserId(User);

            return View(LotteryRepository.GetUserLotteries(userId));
        }

        [HttpGet]
        public IActionResult AddLottery()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLottery([FromBody]Lottery lotteryData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.GetUserAsync(User);
                var lottery = new Lottery() { Admin = user, Name = lotteryData.Name, Santas=lotteryData.Santas };
                LotteryRepository.Add(lottery);
                foreach (var santa in lottery.Santas)
                {   
                    var userExists = await UserManager.FindByEmailAsync(santa.Email);
                    if(userExists!=null)
                    {
                        //to nigdzie nie przekierowuje ze względu na ajax
                        continue;
                        //ViewBag.InfoTitle = "Użytkownik już istnieje";
                        //ViewBag.InfoContent = "Szukany użytkownik istnieje już w bazie.";
                        //return View("Info");
                    }

                    var userFromSanta = new AppUser { UserName = santa.Email, Email = santa.Email };
                    // NuGet: PasswordGenerator by Paul Seal
                    var pwd = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial().IncludeNumeric().LengthRequired(10);
                    var password = pwd.Next();
                    var result = await UserManager.CreateAsync(userFromSanta, password);

                    
                    if (result.Succeeded)
                    {
                        var token = await UserManager.GenerateEmailConfirmationTokenAsync(userFromSanta);
                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                            new { userId = userFromSanta.Id, token = token }, Request.Scheme);
                    
                        Logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, userFromSanta.Email + " ||| "+ password + " ||| "  + confirmationLink);
                    }
                        
                    
                }

                return RedirectToAction("Info", "Account");
                   // return RedirectToAction("Register", "Account", new { modelList = modelList, islotteryMember = true });
            }

                return RedirectToAction("Index");
            

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult LotteryDetails(int id)
        {
            Lottery lottery = LotteryRepository.GetLottery(id);

            List <String> lista = LotteryRepository.GetAllSantasFromAllLotteries();
            return View(lottery);
        }
    }
}
