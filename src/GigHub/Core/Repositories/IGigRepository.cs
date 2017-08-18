using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithArtistWithFollowersAndAttendances(int gigId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        IEnumerable<Gig> GetUpcomingGigs(string query);
    }
}