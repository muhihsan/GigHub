﻿using GigHub.Persistence.Data;
using GigHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
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

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
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

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public IEnumerable<Gig> GetUpcomingGigs(string query)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCancelled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query)
                        || g.Genre.Name.Contains(query)
                        || g.Venue.Contains(query));
            }

            return upcomingGigs.ToList();
        }
    }
}
