﻿using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Controllers.Api
{
    [Authorize]
    [Route("api/gigs")]
    public class GigsApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GigsApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = _userManager.GetUserId(User);

            var gig = _context.Gigs
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancelled)
                return NotFound();

            gig.IsCancelled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Gig = gig,
                Type = NotificationType.GigCancelled
            };

            _context.Attendances
                .Where(a => a.GigId == gig.Id)
                .ToList()
                .ForEach(a => a.Attendee.Notify(notification));

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
