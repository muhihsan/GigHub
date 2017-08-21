using GigHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Persistence.EntityConfigurations
{
    public class FollowingConfiguration : IEntityTypeConfiguration<Following>
    {
        public void Configure(EntityTypeBuilder<Following> builder)
        {
            builder.HasKey(c => new { c.FollowerId, c.FolloweeId });
        }
    }
}
