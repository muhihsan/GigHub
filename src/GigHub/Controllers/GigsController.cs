using GigHub.Data;
using GigHub.Models;
using GigHub.Repositories;
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
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GenreRepository _genreRepository;
        private readonly GigRepository _gigRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public GigsController(
            ApplicationDbContext context,
            AttendanceRepository attendanceRepository,
            GenreRepository genreRepository,
            GigRepository gigRepository,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _attendanceRepository = attendanceRepository;
            _genreRepository = genreRepository;
            _gigRepository = gigRepository;
            _userManager = userManager;
        }
        
        public IActionResult Mine()
        {
            var userId = _userManager.GetUserId(User);
            var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);
            return View(gigs);
        }

        public IActionResult Attending()
        {
            var userId = _userManager.GetUserId(User);
            
            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var gig = _gigRepository.GetGigWithArtistWithFollowersAndAttendances(id);

            if (gig == null)
                return NotFound();

            var viewModel = new GigDetailsViewModel(gig);

            if (User.Identity.IsAuthenticated)
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
                Genres = _genreRepository.GetGenres(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        public IActionResult Update(int id)
        {
            var gig = _gigRepository.GetGig(id);

            if (gig == null)
                return NotFound();

            if (gig.ArtistId != _userManager.GetUserId(User))
                return Unauthorized();

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Genres = _genreRepository.GetGenres(),
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
                viewModel.Genres = _genreRepository.GetGenres();
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
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }
            
            var gig = _gigRepository.GetGigWithAttendees(viewModel.Id);
            if (gig == null)
                return NotFound();

            if (gig.ArtistId != _userManager.GetUserId(User))
                return Unauthorized();

            gig.Modify(viewModel.Venue, viewModel.GetDateTime(), viewModel.Genre);
            
            _context.SaveChanges();

            return RedirectToAction("Mine");
        }
    }
}
