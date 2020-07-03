using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Repositories
{
    public interface IRecommendationRepository
    {
        Task<IEnumerable<Recommendation>> GetUserRecommendationsAsync(User user, int page, int length);
        Task<int> UserRecommendationsCountAsync(User user);
    }
}
