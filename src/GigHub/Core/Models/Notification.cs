using System;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        public Gig Gig { get; private set; }
        
        public Notification()
        {
        }

        private Notification(NotificationType type, Gig gig)
        {
            Gig = gig ?? throw new ArgumentNullException("gig");
            Type = type;
            DateTime = DateTime.Now;
        }

        private Notification(NotificationType type, Gig gig, DateTime originalDateTime, string originalVenue)
            : this(type, gig)
        {
            OriginalDateTime = originalDateTime;
            OriginalVenue = originalVenue;
        }

        public static Notification GigCreated(Gig newGig)
        {
            return new Notification(NotificationType.GigCreated, newGig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            return new Notification(NotificationType.GigUpdated, newGig, originalDateTime, originalVenue);
        }

        public static Notification GigCancelled(Gig newGig)
        {
            return new Notification(NotificationType.GigCancelled, newGig);
        }
    }
}
