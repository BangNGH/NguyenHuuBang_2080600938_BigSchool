using Microsoft.AspNet.Identity;
using NguyenHuuBang_2080600938.Models;
using NguyenHuuBang_2080600938.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
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
                categories = _dbContext.Categories.ToList(),
                Heading = "Add Course"
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
            return RedirectToAction("MineCourse", "Courses");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Leturer)
                .Include(l => l.category).ToList();
            var viewModel = new CourseViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
        [Authorize]
        public ActionResult LecturerFollowing()
        {
            var userId = User.Identity.GetUserId();
            var Lecturers = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.FolloweeId).ToList();
            List<string> users = new List<string>();
            foreach (var lecturerId in Lecturers)
            {
                var name = _dbContext.Users.Where(u => u.Id == lecturerId).Select(a => a.Name).FirstOrDefault();
                if (!string.IsNullOrEmpty(name))
                {
                    users.Add(name);
                }
            }

            var viewModel = new LectureViewModel
            {
                Users = users
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult MineCourse()
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course
                .Where(z => z.LeturerId == userId && z.DateTime > System.DateTime.Now)
                .Include(l => l.Leturer)
                .Include(c => c.category)
                .ToList();
            return View(course);
        }
        [Authorize]
        public ActionResult Edit(int Id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.SingleOrDefault(c => c.Id == Id && c.LeturerId == userId);
            var viewModel = new CourseViewModel
            {
                categories = _dbContext.Categories.ToList(),
                Date = course.DateTime.ToString("dd/M/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                Category = course.CategoryId,
                Place = course.Place,
                Heading = "Edit Course",
                Id = course.Id,
            };
            return View("Create", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel vModel)
        {
            if (!ModelState.IsValid)
            {
                vModel.categories = _dbContext.Categories.ToList();
                return View("Create", vModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.SingleOrDefault(c => c.Id == vModel.Id && c.LeturerId == userId);
            course.Place = vModel.Place;
            course.DateTime = vModel.GetDateTime();
            course.CategoryId = vModel.Category;
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult EnableCourse(int Id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.SingleOrDefault(c => c.Id == Id);
            course.IsCanceled = false;
            _dbContext.SaveChanges();
            TempData["Message"] = "Course enabled successfully!";
            return RedirectToAction("MineCourse", "Courses");
        }
    }
}