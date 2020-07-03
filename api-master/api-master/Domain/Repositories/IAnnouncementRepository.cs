using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> ListAsync(int offset, int length);
        Task AddAnnouncementAsync(Announcement announcement);
        Task RemoveAnnouncementAsync(int announcementId);
        void UpdateAnnouncement(Announcement announcement);
        Task<Announcement> FindByIdAsync(int announcementId);
        Task<IEnumerable<Announcement>> GetUserAnnouncementsAsync(User user, int offset, int length);
        Task<int> CountAsync();
        Task<int> UserAnnouncementsCountAsync(User user);
        Task<IEnumerable<Announcement>> SearchAsync(string text, int offset, int length);
        Task<int> SearchCountAsync(string text);
    }
}
