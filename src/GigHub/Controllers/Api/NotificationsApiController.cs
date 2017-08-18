using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    [Route("api/notifications")]
    public class NotificationsApiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationsApiController(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet("")]
        public IActionResult GetNewNotifications()
        {
            var userId = _userManager.GetUserId(User);
            var notifications = _unitOfWork.Notifications.GetNewNotificationsFor(userId);
            return Ok(notifications.Select(Mapper.Map<Notification, NotificationDto>));
        }

        [HttpPost("markAsRead")]
        public async Task<IActionResult> MarkAsRead()
        {
            var userId = _userManager.GetUserId(User);

            var notifications = _unitOfWork.UserNotifications.GetUserNotificationsFor(userId);
            notifications.ToList().ForEach(n => n.Read());

            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
