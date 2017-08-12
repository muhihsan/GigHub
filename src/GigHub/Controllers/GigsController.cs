using GigHub.Data;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GigHub.Controllers
{
    [Authorize]
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public GigsController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public IActionResult Mine()
        {
            var userId = _userManager.GetUserId(User);

            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCancelled)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        public IActionResult Attending()
        {
            var userId = _userManager.GetUserId(User);

            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", viewModel);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .ThenInclude(a => a.Followers)
                .Include(g => g.Attendances)
                .SingleOrDefault(g => g.Id == id);

            if (gig == null)
                return NotFound();

            var viewModel = new GigDetailsViewModel(gig);

            if (_signInManager.IsSignedIn(User))
                viewModel.GetUserInfo(_userManager.GetUserId(User));

            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public IActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        public IActionResult Update(int id)
        {
            var userId = _userManager.GetUserId(User);

            var gig = _context.Gigs
                .Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };
            return View("GigForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = _userManager.GetUserId(User),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = _userManager.GetUserId(User);

            var gig = _context.Gigs
                .Include(g => g.Attendances)
                .ThenInclude(a => a.Attendee)
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.Venue, viewModel.GetDateTime(), viewModel.Genre);
            
            _context.SaveChanges();

            return RedirectToAction("Mine");
        }
    }
}
