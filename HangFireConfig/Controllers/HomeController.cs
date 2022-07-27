using Hangfire;
using HangFireConfig.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HangFireConfig.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecurringJobManager recurringJobManager;

        public HomeController(ILogger<HomeController> logger , IRecurringJobManager recurringJobManager)
        {
            _logger = logger;
            this.recurringJobManager = recurringJobManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("BackUp")]
        public IActionResult BackUp(string userName)
        {
            recurringJobManager.AddOrUpdate("test", () => BackUpDataBase(), Cron.Weekly);
            return Ok();
        }

        public void BackUpDataBase()
        {
            // ...
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
