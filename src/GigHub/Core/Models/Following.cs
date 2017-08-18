using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class Following
    {
        [Column(Order = 1)]
        public string FollowerId { get; set; }

        [Column(Order = 2)]
        public string FolloweeId { get; set; }

        public ApplicationUser Follower { get; set; }

        public ApplicationUser Followee { get; set; }
    }
}
