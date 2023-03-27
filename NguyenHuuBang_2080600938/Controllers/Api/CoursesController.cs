using Microsoft.AspNet.Identity;
using NguyenHuuBang_2080600938.Models;
using System.Linq;
using System.Web.Http;

namespace NguyenHuuBang_2080600938.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _dbContext { get; set; }
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int Id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.SingleOrDefault(c => c.Id == Id && c.LeturerId == userId);
            if (course.IsCanceled)
            {
                return Ok();
            }
            course.IsCanceled = true;
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
