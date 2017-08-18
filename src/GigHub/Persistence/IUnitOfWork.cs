using System.Threading.Tasks;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }

        void Complete();
        Task CompleteAsync();
    }
}