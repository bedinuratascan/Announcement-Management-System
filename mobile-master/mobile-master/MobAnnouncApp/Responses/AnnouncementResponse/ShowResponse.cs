using System;
using System.Net;
using MobAnnouncApp.Models.Announcement;
using MobAnnouncApp.Services;
using Newtonsoft.Json;

namespace MobAnnouncApp.Responses.AnnouncementResponse
{
    public struct ShowResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public ShowAnnouncement? Data { get; set; }

        public ShowResponse(Response response)
        {
            StatusCode = response.StatusCode;

            if (HttpStatusCode.OK.Equals(response.StatusCode) ||
                HttpStatusCode.Created.Equals(response.StatusCode))
                Data = JsonConvert.DeserializeObject<ShowAnnouncement>(response.Data);
            else
                Data = null;
        }
    }
}
