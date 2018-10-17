using System.Threading.Tasks;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        void Add(Following following);
        Task AddAsycn(Following following);
        Following GetFollowing(string followerId, string followeeId);
        void Remove(Following following);
    }
}