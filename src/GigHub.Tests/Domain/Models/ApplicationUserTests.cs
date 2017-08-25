using FluentAssertions;
using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GigHub.Tests.Domain.Models
{
    public class ApplicationUserTests
    {
        public static IEnumerable<object[]> GetNotifications
            => new[]
                {
                    new object[] { Notification.GigCancelled(new Gig()) },
                    new object[] { Notification.GigCreated(new Gig()) },
                    new object[] { Notification.GigUpdated(new Gig(), DateTime.MinValue, "") }
                };

        [Theory]
        [MemberData(nameof(GetNotifications))]
        public void Notify_WhenCalled_ShouldAddTheNotification(Notification notification)
        {
            var user = new ApplicationUser();

            user.Notify(notification);

            user.UserNotifications.Count.Should().Be(1);

            var userNotification = user.UserNotifications.First();
            userNotification.Notification.Should().Be(notification);
            userNotification.User.Should().Be(user);
        }
    }
}
