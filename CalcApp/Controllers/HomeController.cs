using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CalcApp.Models;

namespace CalcApp
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //return View();


            //return RedirectToAction("Login", "LoginController");
            return View("Login");
        }

      //  public IActionResult Privacy()
        //{
        //    return View();
       // }
       // private TapCountContext db;
      //  public HomeController(TapCountContext EFContext)
      //  {
           // db = EFContext;
      //  }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
