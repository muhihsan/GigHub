using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    [Route("api/followings")]
    public class FollowingsApiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public FollowingsApiController(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager)
        {
           _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromBody] FollowingDto dto)
        {
            var userId = _userManager.GetUserId(User);

            var following = _unitOfWork.Followings.GetFollowing(userId, dto.FolloweeId);
            if (following != null)
                return BadRequest("Following already exists.");

            following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            await _unitOfWork.Followings.AddAsycn(following);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete("{followeeId}")]
        public async Task<IActionResult> Unfollow(string followeeId)
        {
            var userId = _userManager.GetUserId(User);

            var following = _unitOfWork.Followings.GetFollowing(userId, followeeId);

            if (following == null)
                return NotFound();

            _unitOfWork.Followings.Remove(following);
            await _unitOfWork.CompleteAsync();

            return Ok(followeeId);

        }
    }
}
