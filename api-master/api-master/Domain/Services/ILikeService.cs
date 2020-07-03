using AnnouncApp.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Services
{
    public interface ILikeService
    {
        Task<Like> AddLikeAsync(User user, Announcement announcement);
        Task RemoveLikeAsync(User user, Announcement announcement);
        Task<IEnumerable<Like>> GetUserLikesAsync(User user, int offset, int length);
        Task<int> UserLikesCountAsync(User user);
    }
}