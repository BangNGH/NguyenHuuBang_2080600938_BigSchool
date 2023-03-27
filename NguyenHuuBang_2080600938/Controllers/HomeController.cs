using Microsoft.AspNet.Identity;
using NguyenHuuBang_2080600938.Models;
using NguyenHuuBang_2080600938.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace NguyenHuuBang_2080600938.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var upComingCourses = _dbContext.Course
                .Where(x => x.IsCanceled == false)
                .Include(x => x.Leturer)
                .Include(x => x.category)
                .Where(x => x.DateTime > DateTime.Now);
            var lecturersFollowedId = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.FolloweeId).ToList();
            var attendanceCoursesId = _dbContext.Attendances
               .Where(a => a.AttendeeId == userId)
               .Select(a => a.CourseId).ToList();
            var viewModel = new CourseViewModel
            {
                UpcommingCourses = upComingCourses,
                AttendanceCourses = attendanceCoursesId,
                lecturersFollowed = lecturersFollowedId,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}