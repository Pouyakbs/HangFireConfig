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
        private readonly IBackgroundJobClient backgroundJobClient;

        public HomeController(ILogger<HomeController> logger , IRecurringJobManager recurringJobManager , IBackgroundJobClient backgroundJobClient)
        {
            _logger = logger;
            this.recurringJobManager = recurringJobManager;
            this.backgroundJobClient = backgroundJobClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("welcome")]
        public IActionResult Welcome()
        {
            var jobId = backgroundJobClient.Enqueue(() => SendWelcomeMail("Pouyakhodabakhsh1994@gmail.com"));
            return Ok($"Job Id {jobId} Completed. Welcome Mail Sent!");
        }

        public void SendWelcomeMail(string username)
        {
            //Logic to Mail the user
            Console.WriteLine($"Welcome to our application, Pouyakhodabakhsh1994@gmail.com");
        }

        [Route("BackUp")]
        public IActionResult BackUp()
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
