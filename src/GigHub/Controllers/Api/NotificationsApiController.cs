using AutoMapper;
using GigHub.Data;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GigHub.Controllers.Api
{
    [Authorize]
    [Route("api/notifications")]
    public class NotificationsApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationsApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetNewNotifications()
        {
            var userId = _userManager.GetUserId(User);

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist);

            return Ok(notifications.Select(Mapper.Map<Notification, NotificationDto>));
        }
    }
}
