using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Repositories
{

    public class AnnouncementRepository : BaseRepository, IAnnouncementRepository
    {
        public AnnouncementRepository(AnnouncAppDBContext context) : base(context) { }

        public async Task AddAnnouncementAsync(Announcement announcement)
        {
            await context.Announcement.AddAsync(announcement);
        }

        public async Task<Announcement> FindByIdAsync(int announcementId)
        {
            return await context.Announcement
                .Include(a => a.User)
                .Include(a => a.Likes)
                .ThenInclude(l => l.User)
                .SingleOrDefaultAsync(a => a.Id == announcementId);
        }

        public async Task<IEnumerable<Announcement>> ListAsync(int offset, int length)
        {
            return await context.Announcement
                .Include(a => a.User)
                .Include(a => a.Likes)
                .ThenInclude(l => l.User)
                .OrderByDescending(a => a.CreatedAt)
                .Skip(offset)
                .Take(length)
                .ToListAsync();
        }

        public async Task RemoveAnnouncementAsync(int announcementId)
        {
            var announcement = await FindByIdAsync(announcementId);
            context.Announcement.Remove(announcement);
        }

        public void UpdateAnnouncement(Announcement announcement)
        {
            context.Announcement.Update(announcement);
        }

        public async Task<IEnumerable<Announcement>> GetUserAnnouncementsAsync(User user, int offset, int length)
        {
            return await context.Announcement
                .Include(a => a.Likes)
                .Where(a => a.User.Equals(user))
                .Skip(offset)
                .Take(length)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await context.Announcement.CountAsync();
        }

        public async Task<int> UserAnnouncementsCountAsync(User user)
        {
            return await context.Announcement
                .Where(a => a.User.Equals(user))
                .CountAsync();
        }

        public async Task<IEnumerable<Announcement>> SearchAsync(string text, int offset, int length)
        {
            return await context.Announcement
                .Include(a => a.User)
                .Include(a => a.Likes)
                .Where(a => a.Contents.ToLower().Contains(text.ToLower()) || a.Title.ToLower().Contains(text.ToLower()))
                .Skip(offset)
                .Take(length)
                .ToListAsync();
        }

        public async Task<int> SearchCountAsync(string text)
        {
            return await context.Announcement
                .Where(a => a.Contents.ToLower().Contains(text.ToLower()) || a.Title.ToLower().Contains(text.ToLower()))
                .CountAsync();
        }
    }
}
