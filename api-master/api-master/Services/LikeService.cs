
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
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository likeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IInteractionService interactionService;

        public LikeService(ILikeRepository likeRepository, IUnitOfWork unitOfWork, IInteractionService interactionService)
        {
            this.likeRepository = likeRepository;
            this.unitOfWork = unitOfWork;
            this.interactionService = interactionService;
        }


        public async Task<Like> AddLikeAsync(User user, Announcement announcement)
        {
            Like like = new Like
            {
                User = user,
                Announcement = announcement
            };

            await likeRepository.AddLikeAsync(like);
            await interactionService.AddInteractionAsync(user, announcement, Interaction.InteractionType.LIKE);
            await unitOfWork.CompleteAsync();

            return like;
        }

        public async Task RemoveLikeAsync(User user, Announcement announcement)
        {
            Like like = await likeRepository.FindAsync(user, announcement);

            likeRepository.RemoveLike(like);
            await interactionService.RemoveInteractionAsync(user, announcement, Interaction.InteractionType.LIKE);
            await unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Like>> GetUserLikesAsync(User user, int offset, int length)
        {
            IEnumerable<Like> likes = await likeRepository.GetUserLikesAsync(user, offset, length);

            return likes;
        }

        public async Task<int> UserLikesCountAsync(User user)
        {
            return await likeRepository.UserLikesCountAsync(user);
        }
    }
}
