using GigHub.Core.Models;
using GigHub.Persistence.Data;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Following> GetForUser(string userId)
        {
            return _context.Followings
               .Where(f => f.FollowerId == userId)
               .ToList();
        }
    }
}