
using AnnouncApp.Domain;
using AnnouncApp.Domain.Repositories;
using AnnouncApp.Domain.Responses;
using AnnouncApp.Domain.Services;
using AnnouncApp.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository RecommendationRepository;
        private readonly IUnitOfWork UnitOfWork;

        public RecommendationService(IRecommendationRepository recommendationRepository, IUnitOfWork unitOfWork)
        {
            this.RecommendationRepository = recommendationRepository;
            this.UnitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Recommendation>> GetUserRecommendationsAsync(User user, int offset, int length)
        {
            var recommendations = await RecommendationRepository.GetUserRecommendationsAsync(user, offset, length);

            return recommendations;
        }

        public async Task<int> UserRecommendationsCountAsync(User user)
        {
            return await RecommendationRepository.UserRecommendationsCountAsync(user);
        }
    }
}
