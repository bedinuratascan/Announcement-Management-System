using System.Collections.Generic;
using System.Net;
using MobAnnouncApp.Services;
using Newtonsoft.Json;
using static MobAnnouncApp.Models.Announcement.ListAnnouncement;

namespace MobAnnouncApp.Responses.User
{
    public struct LikeListResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public PageData? Data { get; set; }

        public LikeListResponse(Response response)
        {
            StatusCode = response.StatusCode;

            if (HttpStatusCode.OK.Equals(response.StatusCode))
                Data = JsonConvert.DeserializeObject<PageData>(response.Data);
            else
                Data = null;
        }

        public struct PageData
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
            public List<Like> Likes { get; set; }

            public struct Like
            {
                [JsonProperty("id")]
                public int Id { get; set; }
                [JsonProperty("title")]
                public string Title { get; set; }
                [JsonProperty("user")]
                public string User { get; set; }
                [JsonProperty("likeCount")]
                public int LikeCount { get; set; }
                [JsonIgnore]
                public LikeStatus LikeStatus => new LikeStatus()
                {
                    FontFamily = "fa-solid-900.ttf#Font Awesome 5 Free",
                    FontColor = "Red"
                };
                [JsonIgnore]
                public string LikeCountText => $"{LikeCount} Beğeni";
            }
        }
    }
}
