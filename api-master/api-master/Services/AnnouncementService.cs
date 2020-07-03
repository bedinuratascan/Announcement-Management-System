using AnnouncApp.Domain;
using AnnouncApp.Domain.Repositories;
using AnnouncApp.Domain.Services;
using AnnouncApp.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncApp.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository announcementRepository;
        private readonly IUnitOfWork unitOfWork;

        public AnnouncementService(IAnnouncementRepository announcementRepository, IUnitOfWork unitOfWork)
        {
            this.announcementRepository = announcementRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Announcement> FindByIdAsync(int announcementId)
        {
            Announcement announcement = await announcementRepository.FindByIdAsync(announcementId);

            return announcement;
        }

        public async Task<IEnumerable<Announcement>> ListAsync(int offset, int length)
        {
            IEnumerable<Announcement> announcements = await announcementRepository.ListAsync(offset, length);
            return announcements;
        }

        public async Task<Announcement> AddAnnouncement(Announcement announcement, User CurrentUser)
        {
            announcement.CreatedAt = DateTime.Now;
            announcement.User = CurrentUser;
            await announcementRepository.AddAnnouncementAsync(announcement);
            await unitOfWork.CompleteAsync();

            return announcement;
        }

        public async Task<Announcement> UpdateAnnouncement(Announcement incomingAnnouncement, int announcementId)
        {
            Announcement existingAnnouncement = await announcementRepository.FindByIdAsync(announcementId);
            if (existingAnnouncement == null)
                return null;

            existingAnnouncement.Title = incomingAnnouncement.Title;
            existingAnnouncement.Contents = incomingAnnouncement.Contents;
            existingAnnouncement.UpdatedAt = DateTime.Now;

            announcementRepository.UpdateAnnouncement(existingAnnouncement);
            await unitOfWork.CompleteAsync();

            return existingAnnouncement;
        }

        public async Task<Announcement> RemoveAnnouncement(int announcementId)
        {
            Announcement announcement = await announcementRepository.FindByIdAsync(announcementId);
            if (announcement == null)
                return null;
            
            await announcementRepository.RemoveAnnouncementAsync(announcementId);
            await unitOfWork.CompleteAsync();

            return announcement;
        }

        public async Task<IEnumerable<Announcement>> GetUserAnnouncementsAsync(User user, int offset, int length)
        {
            IEnumerable<Announcement> announcements = await announcementRepository.GetUserAnnouncementsAsync(user, offset, length);

            return announcements;
        }

        public async Task<int> CountAsync()
        {
            return await announcementRepository.CountAsync();
        }

        public async Task<int> UserAnnouncementsCountAsync(User user)
        {
            return await announcementRepository.UserAnnouncementsCountAsync(user);
        }

        public async Task<IEnumerable<Announcement>> SearchAsync(string text, int offset, int length)
        {
            return await announcementRepository.SearchAsync(text, offset, length);
        }

        public async Task<int> SearchCountAsync(string text)
        {
            return await announcementRepository.SearchCountAsync(text);
        }
    }
}
