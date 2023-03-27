using Microsoft.AspNet.Identity;
using NguyenHuuBang_2080600938.DTOs;
using NguyenHuuBang_2080600938.Models;
using System.Linq;
using System.Web.Http;

namespace NguyenHuuBang_2080600938.Controllers
{
    [Authorize]

    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpGet]
        public string Get(int id) { return "value"; }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDTO attendanceDTO)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(x => x.AttendeeId == userId && attendanceDTO.CourseId == x.CourseId))
                return BadRequest("The Attendance already exists");
            var attendance = new Attendance
            {
                CourseId = attendanceDTO.CourseId,
                AttendeeId = userId
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();

        }
        [HttpDelete]
        public IHttpActionResult CancelCourse(int courseId)
        {
            Attendance cancelCourse = new Attendance();
            var attendeeId = User.Identity.GetUserId();
            cancelCourse = _dbContext.Attendances.FirstOrDefault(f => f.CourseId == courseId && f.AttendeeId == attendeeId);
            if (cancelCourse == null)
            {
                return BadRequest("Error!");
            }
            _dbContext.Attendances.Remove(cancelCourse);
            _dbContext.SaveChanges();
            return Ok();
        }


    }
}
