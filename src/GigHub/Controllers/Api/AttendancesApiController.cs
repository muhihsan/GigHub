using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GigHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GigHub.Core.Dtos;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    [Route("api/attendances")]
    public class AttendancesApiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendancesApiController(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        
        [HttpPost("attend")]
        public async Task<IActionResult> Attend([FromBody] AttendanceDto dto)
        {
            if (dto.GigId < 1)
                return BadRequest();

            var userId = _userManager.GetUserId(User);

            var attendance = _unitOfWork.Attendances.GetAttendance(dto.GigId, userId);
            if (attendance != null)
                return BadRequest("The attendance already exists.");

            attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            await _unitOfWork.Attendances.AddAsync(attendance);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete("{gigId}")]
        public async Task<IActionResult> CancelAttandance(int gigId)
        {
            var userId = _userManager.GetUserId(User);

            var attendance = _unitOfWork.Attendances.GetAttendance(gigId, userId);
            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendances.Remove(attendance);
            await _unitOfWork.CompleteAsync();

            return Ok(gigId);
        }
    }
}
