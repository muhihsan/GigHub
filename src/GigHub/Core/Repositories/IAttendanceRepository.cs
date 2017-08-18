using System.Collections.Generic;
using GigHub.Core.Models;
using System.Threading.Tasks;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendance(int gigId, string userId);
        Task AddAsync(Attendance attendance);
        void Remove(Attendance attendance);
    }
}