using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Data;
using System.Threading.Tasks;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAttendanceRepository Attendances { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IGigRepository Gigs { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context,
            IAttendanceRepository attendanceRepository,
            IGenreRepository genreRepository,
            IGigRepository gigRepository)
        {
            _context = context;
            Attendances = attendanceRepository;
            Genres = genreRepository;
            Gigs = gigRepository;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
