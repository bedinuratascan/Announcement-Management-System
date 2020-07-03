using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Repositories
{

    public class RecommendationRepository : BaseRepository, IRecommendationRepository
    {
        public RecommendationRepository(AnnouncAppDBContext context) : base(context) { }

        public async Task<IEnumerable<Recommendation>> GetUserRecommendationsAsync(User user, int offset, int length)
        {
            return await context.Recommendation
                .Include(r => r.User)
                .Include(r => r.Announcement)
                .ThenInclude(a => a.User)
                .Include(r => r.Announcement)
                .ThenInclude(a => a.Likes)
                .Where(r => r.User.Equals(user))
                .Skip(offset)
                .Take(length)
                .ToListAsync();
        }

        public async Task<int> UserRecommendationsCountAsync(User user)
        {
            return await context.Recommendation
                .Where(r => r.User.Equals(user))
                .CountAsync();
        }
    }
}
