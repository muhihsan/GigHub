using GigHub.Persistence.Data;
using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                                .ToList();
        }
    }
}
