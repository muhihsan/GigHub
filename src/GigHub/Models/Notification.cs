using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }

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
    }
}
