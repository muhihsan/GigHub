using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GigHub.Core.Models;
using GigHub.Persistence.EntityConfigurations;

namespace GigHub.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.ApplyConfiguration(new GigConfiguration());

            builder.Entity<Attendance>()
                .HasKey(a => new { a.GigId, a.AttendeeId });

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Followers)
                .WithOne(f => f.Followee)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Followees)
                .WithOne(f => f.Follower)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Following>()
                .HasKey(c => new { c.FollowerId, c.FolloweeId });

            builder.Entity<UserNotification>()
                .HasKey(c => new { c.UserId, c.NotificationId });

            builder.Entity<UserNotification>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserNotifications)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
