using EmailApplication.Services.Helpers;
using EmailApplication.Services.Interfaces;
using EmailApplication.WebUi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmailApplication.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<IActionResult> SendEmail()
        {
            try
            {
                EmailRequest emailRequest = new()
                {
                    ToEmail = "beingaslam@outlook.in",
                    Subject = "Welcome Aslam Shaikh !",
                    Body = "Thanks for reading this email"
                };

                await _emailService.SendEmailAsync(emailRequest);

                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}