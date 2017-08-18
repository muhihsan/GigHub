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
        public IFollowingRepository Followings { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IGigRepository Gigs { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IApplicationUserRepository Users { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context,
            IAttendanceRepository attendanceRepository,
            IGenreRepository genreRepository,
            IGigRepository gigRepository,
            INotificationRepository notificationRepository,
            IApplicationUserRepository applicationUserRepository,
            IUserNotificationRepository userNotificationRepository)
        {
            _context = context;
            Attendances = attendanceRepository;
            Genres = genreRepository;
            Gigs = gigRepository;
            Notifications = notificationRepository;
            Users = applicationUserRepository;
            UserNotifications = userNotificationRepository;
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
