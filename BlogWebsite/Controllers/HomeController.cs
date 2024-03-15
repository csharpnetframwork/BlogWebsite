using BlogWebsite.Models;
using BlogWebsite.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepro blogPostRepro;

        public HomeController(ILogger<HomeController> logger,IBlogPostRepro blogPostRepro)
        {
            _logger = logger;
            this.blogPostRepro = blogPostRepro;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var blogposts= await blogPostRepro.GetAllAsync();
            return View(blogposts);
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
