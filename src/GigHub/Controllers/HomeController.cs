using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GigHub.Models;
using GigHub.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using GigHub.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCancelled);
            
            ILookup<int, Attendance> attendances = null;
            
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);

                attendances = _context.Attendances
                    .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                    .ToList()
                    .ToLookup(a => a.GigId);
            }

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query)
                        || g.Genre.Name.Contains(query)
                        || g.Venue.Contains(query));
            }
            
            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Gigs", viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
