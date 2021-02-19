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
using System.Linq;


namespace WhosYourSanta.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        public UserManager<AppUser> UserManager { get; }
        public ILotteryRepository LotteryRepository { get; }

        public SignInManager<AppUser> SignInManager { get; }
        public ISantaRepository SantaRepository { get; }

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, ILotteryRepository lotteryRepository, SignInManager<AppUser> signInManager, ISantaRepository santaRepository)
        {
            Logger = logger;
            UserManager = userManager;
            LotteryRepository = lotteryRepository;
            SignInManager = signInManager;
            SantaRepository = santaRepository;
        }



        [AllowAnonymous]
        public IActionResult Index()
        {
            if (SignInManager.IsSignedIn(User))
                return RedirectToAction("Main");
            else
                return View();
        }

        public async Task<IActionResult> Main()
        {
            var userId = UserManager.GetUserId(User);
            //return View(LotteryRepository.GetUserLotteries(userId));

            //AppUser appUser = await UserManager.GetUserAsync(User);
            return View(SantaRepository.GetAllUsersLotteries(userId));
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
                //lotteryData.Admin = user;
                var lottery = new Lottery() { Admin = user, Name = lotteryData.Name, Santas = lotteryData.Santas };
                LotteryRepository.Add(lottery);

                foreach (var santa in lottery.Santas)
                {   
                    santa.Lottery = lottery;
                    var userExists = SantaRepository.GetAppUserByEmail(santa.Email);

                    if(userExists!=null)
                    {
                        santa.AppUser = userExists;
                        userExists.Santas.Add(santa);
                        SantaRepository.Update(santa);
                        continue;

                         //to nigdzie nie przekierowuje ze względu na ajax                       
                        //ViewBag.InfoTitle = "Użytkownik już istnieje";
                        //ViewBag.InfoContent = "Szukany użytkownik istnieje już w bazie.";
                        //return View("Info");
                    }

                    var userFromSanta = new AppUser { UserName = santa.Email, Email = santa.Email };
                    //navigation
                    userFromSanta.Santas.Add(santa);
                    santa.AppUser = userFromSanta;

                    // NuGet: PasswordGenerator by Paul Seal
                    //var pwd = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial().IncludeNumeric().LengthRequired(10);
                    //var password = pwd.Next();
                    var password = "Haslo_1";
                    //tworzymy użytkownika i równocześnie loterię
                    var result = await UserManager.CreateAsync(userFromSanta, password);


                    if (result.Succeeded)
                    {
                        var token = await UserManager.GenerateEmailConfirmationTokenAsync(userFromSanta);
                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                            new { userId = userFromSanta.Id, token = token }, Request.Scheme);

                        Logger.Log(Microsoft.Extensions.Logging.LogLevel.Warning, userFromSanta.Email + " ||| " + password + " ||| " + confirmationLink);
                    }
                  
                }

                //return RedirectToAction("Info", "Account");
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

        [HttpGet]
        public IActionResult DrawSanta(int LotteryId)
        {
            // Lottery lottery = LotteryRepository.GetLottery(LotteryId);
            Santa mySanta = SantaRepository.GetSantaBy(UserManager.GetUserId(User),LotteryId);
            if (mySanta.DrawnSanta==null)
            {
                List<Santa> santas = SantaRepository.GetAllSantas(LotteryId);
                return View(santas);
            }

            return RedirectToAction("DrawnSanta", "Home", new { LotteryId = LotteryId});

        }

      
        public IActionResult DrawnSanta(int LotteryId)
        {
            Santa santa;
            Santa mySanta = SantaRepository.GetSantaBy(UserManager.GetUserId(User), LotteryId);

            if (mySanta.DrawnSanta == null)
                santa = SantaRepository.DrawSantaForUser(UserManager.GetUserId(User), LotteryId);
            else
                santa = mySanta.DrawnSanta;



            //var name = SantaList.Select(s => s.Name).FirstOrDefault();
            //Santa santa = SantaList.Where(s => s.Name == name).FirstOrDefault();

            return View(santa);
        }
    }
}
