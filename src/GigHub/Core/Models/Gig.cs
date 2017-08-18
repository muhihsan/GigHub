using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; private set; }

        public bool IsCancelled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Modify(string venue, DateTime dateTime, byte genreId)
        {
            NotifyAttendences(Notification.GigUpdated(this, DateTime, Venue));
            Venue = venue;
            DateTime = dateTime;
            GenreId = genreId;
        }

        public void Cancel()
        {
            NotifyAttendences(Notification.GigCancelled(this));
            IsCancelled = true;
        }

        private void NotifyAttendences(Notification notification)
        {
            Attendances.ToList().ForEach(a => a.Attendee.Notify(notification));
        }
    }
}
