using System;
using AnnouncApp.Domain;

namespace AnnouncApp.Serializer.AnnouncementSerializer
{
    public class CreateSerializer
    {
        public CreateData Response { get; set; }

        public CreateSerializer(Announcement announcement, User CurrentUser)
        {
            Response = new CreateData(announcement, CurrentUser);
        }
    }

    public struct CreateData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
        public string Contents { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CreateData(Announcement announcement, User currentUser)
        {
            Id = announcement.Id;
            Title = announcement.Title;
            User = $"{currentUser.Name} {currentUser.SurName}";
            LikeCount = 0;
            IsLiked = false;
            Contents = announcement.Contents;
            CreatedAt = announcement.CreatedAt;
            UpdatedAt = announcement.UpdatedAt;
        }
    }
}