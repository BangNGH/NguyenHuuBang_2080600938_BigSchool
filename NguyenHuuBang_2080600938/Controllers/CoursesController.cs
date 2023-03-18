using Microsoft.AspNet.Identity;
using NguyenHuuBang_2080600938.Models;
using NguyenHuuBang_2080600938.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace NguyenHuuBang_2080600938.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                categories = _dbContext.Categories.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LeturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Course.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}