using System;
using AnnouncApp.Domain;

namespace AnnouncApp.Serializer.AnnouncementSerializer
{
    public class ShowSerializer
    {
        public ShowData Response { get; set; }

        public ShowSerializer(Announcement announcement, User currentUser)
        {
            Response = new ShowData(announcement, currentUser);
        }
    }

    public struct ShowData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
        public string Contents { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsOwn { get; set; }

        public ShowData(Announcement announcement, User currentUser)
        {
            Id = announcement.Id;
            Title = announcement.Title;
            User = $"{announcement.User.Name} {announcement.User.SurName}";
            LikeCount = announcement.Likes.Count;
            IsLiked = announcement.Likes.Exists(l => l.User.Equals(currentUser));
            Contents = announcement.Contents;
            CreatedAt = announcement.CreatedAt;
            UpdatedAt = announcement.UpdatedAt;
            IsOwn = announcement.User.Equals(currentUser);
        }
    }
}