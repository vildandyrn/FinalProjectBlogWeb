using BlogProjectWeb.Models;
using BlogProjectWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BlogProjectWeb.Models.Domain;

namespace BlogProjectWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository blogRepository;
        public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepository)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await blogRepository.GetAllAsync();
            return View(blogPosts);
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