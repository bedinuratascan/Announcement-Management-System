using System;
using System.Collections.Generic;
using AnnouncApp.Domain;

namespace AnnouncApp.Serializer.UserSerializer
{
    public struct RecommendationsSerializer
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public int Total { get; set; }
        public bool HasMore { get; set; }
        public List<RecommendationData> Data { get; set; }

        public RecommendationsSerializer(int offset, int length, int total, IEnumerable<Recommendation> recommendations)
        {
            Offset = offset;
            Length = length;
            Total = total;
            HasMore = offset + length < total;
            Data = new List<RecommendationData>();
            foreach (var recommendation in recommendations)
            { 
                Data.Add(new RecommendationData(recommendation));
            }
        }
    }

    public struct RecommendationData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public RecommendationData(Recommendation recommendation)
        {
            Id = recommendation.Announcement.Id;
            Title = recommendation.Announcement.Title;
            User = $"{recommendation.Announcement.User.Name} {recommendation.Announcement.User.SurName}";
            LikeCount = recommendation.Announcement.Likes.Count;
            CreatedAt = recommendation.Announcement.CreatedAt;
            UpdatedAt = recommendation.Announcement.UpdatedAt;
        }
    }
}