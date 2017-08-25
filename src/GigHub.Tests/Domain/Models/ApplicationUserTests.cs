using FluentAssertions;
using GigHub.Core.Models;
using System.Linq;
using Xunit;

namespace GigHub.Tests.Domain.Models
{
    public class ApplicationUserTests
    {
        [Fact]
        public void Notify_WhenCalled_ShouldAddTheNotification()
        {
            var user = new ApplicationUser();
            var notification = Notification.GigCancelled(new Gig());

            user.Notify(notification);

            user.UserNotifications.Count.Should().Be(1);

            var userNotification = user.UserNotifications.First();
            userNotification.Notification.Should().Be(notification);
            userNotification.User.Should().Be(user);
        }
    }
}
