using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Repositories
{
    public interface ILikeRepository
    {
        Task AddLikeAsync(Like like);
        void RemoveLike(Like like);
        Task<Like> FindAsync(User user, Announcement announcement);
        Task<IEnumerable<Like>> GetUserLikesAsync(User user, int offset, int length);
        Task<int> UserLikesCountAsync(User user);
    }
}