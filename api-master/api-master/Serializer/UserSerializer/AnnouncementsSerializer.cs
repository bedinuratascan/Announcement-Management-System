using System;
using System.Collections.Generic;
using AnnouncApp.Domain;

namespace AnnouncApp.Serializer.UserSerializer
{
    public struct AnnouncementsSerializer
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public int Total { get; set; }
        public bool HasMore { get; set; }
        public List<AnnouncementData> Data { get; set; }

        public AnnouncementsSerializer(int offset, int length, int total, IEnumerable<Announcement> announcements)
        {
            Offset = offset;
            Length = length;
            Total = total;
            HasMore = offset + length < total;
            Data = new List<AnnouncementData>();
            foreach (var announcement in announcements)
            {
                Data.Add(new AnnouncementData(announcement));
            }
        }
    }

    public struct AnnouncementData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public AnnouncementData(Announcement announcement)
        {
            Id = announcement.Id;
            Title = announcement.Title;
            LikeCount = announcement.Likes.Count;
            CreatedAt = announcement.CreatedAt;
            UpdatedAt = announcement.UpdatedAt;
        }
    }
}