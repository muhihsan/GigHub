using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        List<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithArtistWithFollowersAndAttendances(int gigId);
        Gig GetGigWithAttendees(int gigId);
        List<Gig> GetUpcomingGigsByArtist(string userId);
    }
}