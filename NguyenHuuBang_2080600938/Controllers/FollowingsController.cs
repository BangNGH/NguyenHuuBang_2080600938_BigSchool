using Microsoft.AspNet.Identity;
using NguyenHuuBang_2080600938.DTOs;
using NguyenHuuBang_2080600938.Models;
using System.Linq;
using System.Web.Http;

namespace NguyenHuuBang_2080600938.Controllers
{

    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowinDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(x => x.FollowerId == userId && followingDto.FolloweeId == x.FolloweeId))
                return BadRequest("The Attendance already exists");
            var following = new Following
            {
                FolloweeId = followingDto.FolloweeId,
                FollowerId = userId
            };
            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete]

        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = _dbContext.Followings.FirstOrDefault(f => f.FolloweeId == id && f.FollowerId == userId);
            if (following == null)
            {
                return BadRequest("Error!");
            }
            _dbContext.Followings.Remove(following);
            _dbContext.SaveChanges();
            return Ok();
        }


    }
}
