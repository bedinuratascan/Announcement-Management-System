using AnnouncApp.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Services
{
   public interface IAnnouncementService
    {
        Task<IEnumerable<Announcement>> ListAsync(int offset, int length);
        Task<Announcement> AddAnnouncement(Announcement announcement, User CurrentUser);
        Task<Announcement> RemoveAnnouncement(int announcementId);
        Task<Announcement> UpdateAnnouncement(Announcement announcement, int announcementId);
        Task<Announcement> FindByIdAsync(int announcementId);
        Task<IEnumerable<Announcement>> GetUserAnnouncementsAsync(User user, int offset, int length);
        Task<int> CountAsync();
        Task<int> UserAnnouncementsCountAsync(User user);
        Task<IEnumerable<Announcement>> SearchAsync(string text, int offset, int length);
        Task<int> SearchCountAsync(string text);
    }
}
