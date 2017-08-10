using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }
        
        public Notification()
        {
        }

        public Notification(Gig gig, NotificationType type)
        {
            Gig = gig ?? throw new ArgumentNullException("gig");
            Type = type;
            DateTime = DateTime.Now;
        }

        public Notification(Gig gig, NotificationType type, DateTime originalDateTime, string originalVenue)
            : this(gig, type)
        {
            OriginalDateTime = originalDateTime;
            OriginalVenue = originalVenue;
        }
    }
}
