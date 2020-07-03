using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobAnnouncApp.Models.Announcement
{
    public struct ListAnnouncement
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("length")]
        public int Length { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("hasMore")]
        public bool HasMore { get; set; }
        [JsonProperty("data")]
        public List<Announcement> Announcements { get; set; }

        public struct Announcement
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("user")]
            public string User { get; set; }
            [JsonProperty("likeCount")]
            public int LikeCount { get; set; }
            [JsonProperty("isLiked")]
            public bool IsLiked { get; set; }
            [JsonProperty("createdAt", ItemConverterType = typeof(Helpers.JsonDateTimeConverter))]
            public DateTime CreatedAt { get; set; }
            [JsonIgnore]
            public LikeStatus LikeStatus => new LikeStatus()
            {
                FontFamily = IsLiked ? "fa-solid-900.ttf#Font Awesome 5 Free" : "fa-regular-400.ttf#Font Awesome 5 Free",
                FontColor = IsLiked ? "Red" : "Gray"
            };
            [JsonIgnore]
            public string LikeCountText => $"{LikeCount} Beğeni";
        }

        public struct LikeStatus
        {
            public string FontFamily { get; set; }
            public string FontColor { get; set; }
        }
    }
}
