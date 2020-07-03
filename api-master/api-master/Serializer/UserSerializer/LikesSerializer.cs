using System;
using System.Collections.Generic;
using AnnouncApp.Domain;

namespace AnnouncApp.Serializer.UserSerializer
{
    public struct LikesSerializer
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public int Total { get; set; }
        public bool HasMore { get; set; }
        public List<LikeData> Data { get; set; }

        public LikesSerializer(int offset, int length, int total, IEnumerable<Like> likes)
        {
            Offset = offset;
            Length = length;
            Total = total;
            HasMore = offset + length < total;
            Data = new List<LikeData>();
            foreach (var like in likes)
            {
                Data.Add(new LikeData(like));
            }
        }
    }

    public struct LikeData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public LikeData(Like like)
        {
            Id = like.Announcement.Id;
            Title = like.Announcement.Title;
            User = $"{like.Announcement.User.Name} {like.Announcement.User.SurName}";
            LikeCount = like.Announcement.Likes.Count;
            CreatedAt = like.Announcement.CreatedAt;
            UpdatedAt = like.Announcement.UpdatedAt;
        }
    }
}