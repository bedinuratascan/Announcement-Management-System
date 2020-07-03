using AnnouncApp.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Services
{
    public interface IRecommendationService
    {
        Task<IEnumerable<Recommendation>> GetUserRecommendationsAsync(User user, int offset, int length);
        Task<int> UserRecommendationsCountAsync(User user);
    }
}