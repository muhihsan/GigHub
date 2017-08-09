using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Column(Order = 1)]
        public string UserId { get; }

        [Column(Order = 2)]
        public int NotificationId { get; }

        public ApplicationUser User { get; }

        public Notification Notification { get; }

        public bool IsRead { get; set; }

        protected UserNotification()
        {
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            User = user ?? throw new ArgumentNullException("user");
            Notification = notification ?? throw new ArgumentNullException("notification");
        }
    }
}
