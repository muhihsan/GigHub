using GigHub.Persistence.Data;
using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Repositories;
using System.Threading.Tasks;

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

        public Attendance GetAttendance(int gigId, string userId)
        {
            return _context.Attendances.FirstOrDefault(a => a.AttendeeId == userId && a.GigId == gigId);
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public async Task AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}