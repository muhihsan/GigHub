﻿using GigHub.Data;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Controllers.Api
{
    [Authorize]
    [Route("api/followings")]
    public class FollowingsApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FollowingsApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromBody] FollowingDto dto)
        {
            var userId = _userManager.GetUserId(User);

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
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

        [HttpDelete("{followeeId}")]
        public async Task<IActionResult> Unfollow(string followeeId)
        {
            var userId = _userManager.GetUserId(User);

            var following = _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == followeeId);

            if (following == null)
                return NotFound();

            _context.Followings.Remove(following);
            await _context.SaveChangesAsync();

            return Ok(followeeId);

        }
    }
}
