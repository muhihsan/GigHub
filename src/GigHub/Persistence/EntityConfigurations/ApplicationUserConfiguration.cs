using GigHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Persistence.EntityConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(a => a.Followers)
                .WithOne(f => f.Followee)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Followees)
                .WithOne(f => f.Follower)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
