﻿using GigHub.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Core.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Gig> Gigs { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
    }
}