using GigHub.Data;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FollowingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromBody] FollowingDto dto)
        {
            var userId = _userManager.GetUserId(User);

            if (_context.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("Following already exists.");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            await _context.Followings.AddAsync(following);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
