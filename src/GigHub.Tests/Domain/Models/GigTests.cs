using FluentAssertions;
using GigHub.Core.Models;
using System.Linq;
using Xunit;

namespace GigHub.Tests.Domain.Models
{
    public class GigTests
    {
        [Fact]
        public void Cancel_WhenCalled_ShouldSetIsCanceledToTrue()
        {
            var gig = new Gig();

            gig.Cancel();

            gig.IsCancelled.Should().BeTrue();
        }

        [Fact]
        public void Cancel_WhenCalled_EachAttendeeShouldHaveNotification()
        {
            var gig = new Gig();
            gig.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "1" } });

            gig.Cancel();

            gig.GetAttendee().First().UserNotifications.Count.Should().Be(1);
        }
    }
}
