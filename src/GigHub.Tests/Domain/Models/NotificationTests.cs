using FluentAssertions;
using GigHub.Core.Models;
using System;
using Xunit;

namespace GigHub.Tests.Domain.Models
{
    public class NotificationTests
    {
        [Fact]
        public void GigCreated_WhenCalled_ShouldReturnANotificationForACreatedGig()
        {
            var gig = new Gig();
            var notification = Notification.GigCreated(gig);

            notification.Gig.Should().Be(gig);
            notification.Type.Should().Be(NotificationType.GigCreated);
        }

        [Fact]
        public void GigUpdated_WhenCalled_ShouldReturnANotificationForAUpdatedGig()
        {
            var gig = new Gig();
            var originalDateTime = DateTime.Now;
            var originalVenue = "originalVenue";
            var notification = Notification.GigUpdated(gig, originalDateTime, originalVenue);

            notification.Gig.Should().Be(gig);
            notification.OriginalDateTime.Should().Be(originalDateTime);
            notification.OriginalVenue.Should().Be(originalVenue);
            notification.Type.Should().Be(NotificationType.GigUpdated);
        }

        [Fact]
        public void GigCancelled_WhenCalled_ShouldReturnANotificationForACancelledGig()
        {
            var gig = new Gig();
            var notification = Notification.GigCancelled(gig);

            notification.Gig.Should().Be(gig);
            notification.Type.Should().Be(NotificationType.GigCancelled);
        }
    }
}
