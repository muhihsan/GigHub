using GigHub.Data;
using GigHub.Repositories;
using System.Threading.Tasks;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository Attendances { get; private set; }
        public GenreRepository Genres { get; private set; }
        public GigRepository Gigs { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context,
            AttendanceRepository attendanceRepository,
            GenreRepository genreRepository,
            GigRepository gigRepository)
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
