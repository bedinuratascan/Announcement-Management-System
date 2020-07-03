using System.Collections.Generic;
using System.Net;
using MobAnnouncApp.Services;
using Newtonsoft.Json;
using static MobAnnouncApp.Models.Announcement.ListAnnouncement;

namespace MobAnnouncApp.Responses.User
{
    public struct RecommendationListResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public PageData? Data { get; set; }

        public RecommendationListResponse(Response response)
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
            public List<Recommendation> Recommendations { get; set; }

            public struct Recommendation
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
                    FontFamily = "fa-regular-400.ttf#Font Awesome 5 Free",
                    FontColor = "Gray"
                };
                [JsonIgnore]
                public string LikeCountText => $"{LikeCount} Beğeni";
            }
        }
    }
}
