using System.Threading.Tasks;
using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IApplicationUserRepository Users { get; }

        void Complete();
        Task CompleteAsync();
    }
}