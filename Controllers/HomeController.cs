using BlogProject.Data;
using BlogProject.Models;
using BlogProject.Services;
using BlogProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using X.PagedList;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBlogEmailSender emailSender, ApplicationDbContext context)
        {
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;

            var blogs = _context.Blogs
                .Include(b => b.BlogUser)
                .OrderByDescending(b => b.CreatedDate)
                .ToPagedListAsync(pageNumber, pageSize);

            ViewData["HeaderImage"] = "/img/home-bg.jpg";
            ViewData["MainText"] = "Blog Project";
            ViewData["SubText"] = "Simple blog built in ASP.NET Core MVC";


            return View(await blogs);
        }

        public IActionResult About()
        {
            ViewData["HeaderImage"] = "";
            ViewData["MainText"] = "About";
            ViewData["SubText"] = "";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["HeaderImage"] = "";
            ViewData["MainText"] = "Contact Me";
            ViewData["SubText"] = "";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe model)
        {
            model.Message = $"{model.Message} <hr/> Phone: {model.Phone}";
            await _emailSender.SendContactEmailAsync(model.Email, model.Name, model.Subject, model.Message);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}