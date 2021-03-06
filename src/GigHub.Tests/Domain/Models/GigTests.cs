﻿using FluentAssertions;
using GigHub.Core.Models;
using System;
using System.Linq;
using Xunit;

namespace GigHub.Tests.Domain.Models
{
    public class GigTests
    {
        [Fact]
        public void Modify_WhenCalled_ShouldSetVenueDateTimeGenreId()
        {
            var gig = new Gig();
            var newVenue = "NewVenue";
            var newDate = DateTime.Now;
            var genreId = Byte.MinValue;

            gig.Modify(newVenue, newDate, genreId);

            gig.Venue.Should().Be(newVenue);
            gig.DateTime.Should().Be(newDate);
            gig.GenreId.Should().Be(genreId);
        }

        [Fact]
        public void Modify_WhenCalled_EachAttendeeShouldHaveNotification()
        {
            var gig = new Gig();
            gig.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "1" } });
            var newVenue = "NewVenue";
            var newDate = DateTime.Now;
            var genreId = Byte.MinValue;

            gig.Modify(newVenue, newDate, genreId);

            var notifications = gig.GetAttendees().First().UserNotifications;

            notifications.Count.Should().Be(1);
            notifications.First().Notification.Type.Should().Be(NotificationType.GigUpdated);
        }

        [Fact]
        public void Cancel_WhenCalled_ShouldSetIsCancelledToTrue()
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

            var notifications = gig.GetAttendees().First().UserNotifications;

            notifications.Count.Should().Be(1);
            notifications.First().Notification.Type.Should().Be(NotificationType.GigCancelled);
        }
        
        [Fact]
        public void GetAttendees_WhenCalled_ReturnAllAttendees()
        {
            var gig = new Gig();
            gig.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "1" } });

            var attendees = gig.GetAttendees();

            attendees.Count.Should().Be(1);
        }

        [Fact]
        public void GetAttendees_WhenNoAttendancesAndCalled_ReturnEmptyList()
        {
            var gig = new Gig();

            var attendees = gig.GetAttendees();

            attendees.Count.Should().Be(0);
        }
    }
}
