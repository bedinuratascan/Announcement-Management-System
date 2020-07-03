using System;
using Newtonsoft.Json;

namespace MobAnnouncApp.Models.Announcement
{
    public struct ShowAnnouncement
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
        [JsonProperty("contents")]
        public string Contents { get; set; }
        [JsonProperty("createdAt", ItemConverterType = typeof(Helpers.JsonDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updatedAt", ItemConverterType = typeof(Helpers.JsonDateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("isOwn")]
        public bool IsOwn { get; set; }
        [JsonIgnore]
        public LikeStatus LikeStatus => new LikeStatus()
        {
            FontFamily = IsLiked ? "fa-solid-900.ttf#Font Awesome 5 Free" : "fa-regular-400.ttf#Font Awesome 5 Free"
        };
        [JsonIgnore]
        public string LikeCountText => $"{LikeCount} Beğeni";
    }

    public struct LikeStatus
    {
        public string FontFamily { get; set; }

    }
}
