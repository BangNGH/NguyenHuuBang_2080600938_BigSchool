﻿using NguyenHuuBang_2080600938.Models;
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
            var upComingCourses = _dbContext.Course
                .Include(x => x.Leturer)
                .Include(x => x.category)
                .Where(x => x.DateTime > DateTime.Now);
            return View(upComingCourses);
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