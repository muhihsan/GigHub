﻿using GigHub.Data;
using GigHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GigHub.Repositories
{
    public class GigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances)
                .ThenInclude(a => a.Attendee)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public List<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCancelled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithArtistWithFollowersAndAttendances(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .ThenInclude(a => a.Followers)
                .Include(g => g.Attendances)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs.Single(g => g.Id == gigId);
        }
    }
}
