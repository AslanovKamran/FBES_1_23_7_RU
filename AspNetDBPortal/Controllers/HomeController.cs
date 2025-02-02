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
            var posts = _context.Posts.ToList();
            return View(posts);
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

        public IActionResult Details(int id)
        {
            var posts = _context?.Posts.ToList();
            var post = posts?.FirstOrDefault(p => p.Id == id);

            return View(post);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            var posts = _context.Posts.ToList();
            var postToDelete = posts.FirstOrDefault(p => p.Id == id);
            if (postToDelete != null)
            {
                _context.Posts.Remove(postToDelete);
                _context.SaveChanges();
                TempData["Deleted"] = "Post has been deleted!";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
