using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WhosYourSanta.Models;
using WhosYourSanta.ViewModel;

namespace WhosYourSanta.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ILogger<AccountController> logger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            Logger = logger;
        }

        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public ILogger<AccountController> Logger { get; }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Info()
        {
            ViewBag.InfoTitle = "To jest test";
            return View("Info");
        }

        //[HttpPost]
        //public IActionResult LoginTest()
        //{

        //    return View("test");


        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //        var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);


        //        return RedirectToAction("Main", "Home");
        //    //}

        //    //return View(model);


        //}

        [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);

            //check if user exists, email has been confirmed and login and password was entered
            if (user != null && !user.EmailConfirmed &&
                                (await UserManager.CheckPasswordAsync(user, model.Password)))
            {
                ModelState.AddModelError(string.Empty, "Email nie został potwierdzony");
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password,
                                    model.RememberMe, true);

            if (result.Succeeded)
            {
                //here i can add returnUrl
                return RedirectToAction("Main", "home");                   

            }
            
            ModelState.AddModelError(string.Empty, "Niepoprawna próba logowania");
        }

        return View(model);
    }


    [HttpPost]
        public async Task<IActionResult> Logout()
        {
           await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

                if(ModelState.IsValid)
                {

                    var user = new AppUser { UserName = model.Email, Email = model.Email };

                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                                new { userId = user.Id, token = token }, Request.Scheme);

                        Logger.Log(LogLevel.Warning, confirmationLink);


                        ViewBag.InfoTitle = "Dziękuje za rejestrację";
                        ViewBag.InfoContent = "Na podany adres email został wysłany link. Kliknij w go aby potwierdzić założenie konta.";
                        return View("Info");
                        

                        //await SignInManager.SignInAsync(user, isPersistent:false);
                        //return RedirectToAction("Index", "Home");
                    }

                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            
            
           return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                //Change to custom !!!!!!!!!!!!!!!!!!!!!
                //ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                //return View("NotFound");
                ViewBag.InfoTitle = "Taki użytkownik nie istnieje";
                ViewBag.InfoContent = "Szukany użytkownik nie został znaleziony.";
                return View("Info");
            }

            var result = await UserManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                ViewBag.InfoTitle = "Email został potwierdzony";
                ViewBag.InfoContent = "Email " + user.Email +  " został potwierdzony. Zapraszam do logowania.";
                return View("Info");
            }

            //Change to custom !!!!!!!!!!!!!!!!!!!!!
            //ViewBag.ErrorTitle = "Email cannot be confirmed";
            ViewBag.InfoTitle = "Niepoprawny adres Email";
            ViewBag.InfoContent = "Email nie może zostać potwierdzony.";
            return View("Info");
        }
    }
}
