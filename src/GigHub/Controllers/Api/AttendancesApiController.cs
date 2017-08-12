using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GigHub.Dto;

namespace GigHub.Controllers.Api
{
    [Authorize]
    [Route("api/attendances")]
    public class AttendancesApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendancesApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [HttpPost("attend")]
        public async Task<IActionResult> Attend([FromBody] AttendanceDto dto)
        {
            var userId = _userManager.GetUserId(User);

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{gigId}")]
        public async Task<IActionResult> CancelAttandance(int gigId)
        {
            var userId = _userManager.GetUserId(User);

            var attendance = _context.Attendances
                .SingleOrDefault(a => a.AttendeeId == userId && a.GigId == gigId);

            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return Ok(gigId);
        }
    }
}
