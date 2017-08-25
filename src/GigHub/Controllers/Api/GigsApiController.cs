using GigHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    [Route("api/gigs")]
    public class GigsApiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public GigsApiController(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = _userManager.GetUserId(User);

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);
            if (gig == null)
                return NotFound();

            if (gig.IsCancelled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();

            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
