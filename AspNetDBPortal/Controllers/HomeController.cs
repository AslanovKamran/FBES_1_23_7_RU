using System.Diagnostics;
using AspNetDBPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetDBPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsPortalDbContext _context;
        public HomeController(NewsPortalDbContext context) => _context = context;

        public IActionResult Index()
        {
            ViewBag.Posts = _context.Posts.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            try
            {
                _context.Posts.Add(post);
                _context.SaveChanges();

                TempData["Added"] = "Post added successfully";

            }
            catch (Exception ex)
            {

                TempData["Exception"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
