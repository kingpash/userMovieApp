using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using usermovieApp.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace usermovieApp.Controllers
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
            return View();
        }

        public async Task<IActionResult> Privacy()
        {

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("movieApp@gmail.com", "Movie App");
            var subject = "Thank you!";
            var to = new EmailAddress("julieluly.hou@gmail.com", "David");
            var plainTextContent = "We are grateful that you are using our Movie App! Happy Holidays!";
            var htmlContent = "<strong>We are grateful that you are using our Movie App! Happy Holidays!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return View(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
