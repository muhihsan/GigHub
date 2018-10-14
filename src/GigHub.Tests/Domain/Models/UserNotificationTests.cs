using FluentAssertions;
using GigHub.Core.Models;
using System;
using Xunit;

namespace GigHub.Tests.Domain.Models
{
    public class UserNotificationTests
    {
        [Fact]
        public void Initialize_WhenNullUserPassed_ShouldThrowArgumentNullException()
        {
            Action act = () => new UserNotification(null, Notification.GigCreated(new Gig()));

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Initialize_WhenNullNotificationPassed_ShouldThrowArgumentNullException()
        {
            Action act = () => new UserNotification(new ApplicationUser(), null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Read_WhenCalled_ShouldSetIsReadToTrue()
        {
            var userNotification = new UserNotification(new ApplicationUser(), new Notification());

            userNotification.Read();

            userNotification.IsRead.Should().BeTrue();
        }
    }
}
