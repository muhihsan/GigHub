using GigHub.Core.Models;
using System.Linq;

namespace GigHub.ViewModels
{
    public class GigDetailsViewModel
    {
        public Gig Gig { get; private set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }

        public GigDetailsViewModel(Gig gig)
        {
            Gig = gig;
        }

        public void GetUserInfo(string userId)
        {
            IsFollowing = Gig
                .Artist
                .Followers
                .Any(f => f.FollowerId == userId);

            IsAttending = Gig
                .Attendances
                .Any(a => a.AttendeeId == userId);
        }
    }
}
