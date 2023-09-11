using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using wareHouse.Services;
using WareHousePlatForm.APIServices;
using WareHousePlatForm.Models;

namespace WareHousePlatForm.Controllers
{
    [SessionFilter(AllowAction = new[] { "Login", "loginCheck" })]
    public class HomeController : Controller
    {
        ProductAPIService apiProduct = new ProductAPIService();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult loginCheck(UserAccount user)
        {
            var sessionID = HttpContext?.Session.Get<UserAccount>("UserLogin");

            if (sessionID != null)
            {
                return RedirectToAction("Index", "Home");
            }

            var idOnSystem = AppSetting.Account;
            var test = AppSetting.Config;

            if (user.Username == idOnSystem.Username && user.Password == idOnSystem.Password)
            {
                HttpContext.Session.Set<UserAccount>("UserLogin", idOnSystem);

                return RedirectToAction("Index", "Home");

            }

            ViewData["ErrorLogin"] = "Login fail";

            return RedirectToAction("LogIn");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            var data = apiProduct.productBoundPage();
            ViewData["product"] = data;
            return View();
        }

        public ActionResult Borrow( string name , string code , int unit)
        {
            TempData["tool"] = name;
            TempData["code"] = code;
            TempData["unit"] = unit;
            return RedirectToAction("Borrow", "Bound");
        }

        public IActionResult Privacy()
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