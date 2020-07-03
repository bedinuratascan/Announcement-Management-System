using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Repositories
{

    public class LikeRepository : BaseRepository, ILikeRepository
    {
        public LikeRepository(AnnouncAppDBContext context) : base(context) { }

        public async Task AddLikeAsync(Like like)
        {
            await context.Like.AddAsync(like);
        }

        public async Task<Like> FindAsync(User user, Announcement announcement)
        {
            return await context.Like.SingleOrDefaultAsync(ui => ui.Announcement.Equals(announcement) && ui.User.Equals(user));
        }

        public void RemoveLike(Like like)
        {
            context.Like.Remove(like);
        }

        public async Task<IEnumerable<Like>> GetUserLikesAsync(User user, int offset, int length)
        {
            return await context.Like
                .Include(a => a.Announcement)
                .Include(a => a.Announcement.User)
                .Where(a => a.User.Equals(user))
                .Skip(offset)
                .Take(length)
                .ToListAsync();
        }

        public async Task<int> UserLikesCountAsync(User user)
        {
            return await context.Like
                .Where(l => l.User.Equals(user))
                .CountAsync();
        }
    }
}