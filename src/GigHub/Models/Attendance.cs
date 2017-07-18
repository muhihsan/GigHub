using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Attendance
    {
        public Gig Gig { get; set; }

        public ApplicationUser Attendee { get; set; }

        [Column(Order = 1)]
        public int GigId { get; set; }

        [Column(Order = 2)]
        public string AttendeeId { get; set; }
    }
}
