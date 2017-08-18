using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        List<Attendance> GetFutureAttendances(string userId);
    }
}