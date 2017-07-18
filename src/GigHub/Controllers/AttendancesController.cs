using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GigHub.Data;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GigHub.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendancesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Attend([FromBody] int gigId)
        {
            var attendance = new Attendance
            {
                GigId = gigId,
                AttendeeId = _userManager.GetUserId(User)
            };

            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
