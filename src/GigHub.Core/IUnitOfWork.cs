using System.Threading.Tasks;
using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        INotificationRepository Notifications { get; }
        IApplicationUserRepository Users { get; }
        IUserNotificationRepository UserNotifications { get; }

        void Complete();
        Task CompleteAsync();
    }
}