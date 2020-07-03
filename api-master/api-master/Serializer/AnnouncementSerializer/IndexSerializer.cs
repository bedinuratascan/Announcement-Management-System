using System;
using System.Collections.Generic;
using AnnouncApp.Domain;

namespace AnnouncApp.Serializer.AnnouncementSerializer
{
    public struct IndexSerializer
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public int Total { get; set; }
        public bool HasMore { get; set; }
        public List<IndexData> Data { get; set; }

        public IndexSerializer(int offset, int length, int total, IEnumerable<Announcement> announcements, User currentUser)
        {
            Offset = offset;
            Length = length;
            Total = total;
            HasMore = offset + length < total;
            Data = new List<IndexData>();
            foreach (var announcement in announcements)
            {
                Data.Add(new IndexData(announcement, currentUser));
            }
        }
    }
    
    public struct IndexData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IndexData(Announcement announcement, User currentUser)
        {
            Id = announcement.Id;
            Title = announcement.Title;
            User = $"{announcement.User.Name} {announcement.User.SurName}";
            IsLiked = announcement.Likes.Exists(l => l.User.Equals(currentUser));
            LikeCount = announcement.Likes.Count;
            CreatedAt = announcement.CreatedAt;
            UpdatedAt = announcement.UpdatedAt;
        }
    }
}