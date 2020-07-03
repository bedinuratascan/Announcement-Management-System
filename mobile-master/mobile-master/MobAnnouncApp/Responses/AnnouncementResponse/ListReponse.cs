using System;
using System.Net;
using MobAnnouncApp.Models.Announcement;
using MobAnnouncApp.Services;
using Newtonsoft.Json;

namespace MobAnnouncApp.Responses.AnnouncementResponse
{
    public struct ListResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public ListAnnouncement? Data { get; set; }

        public ListResponse(Response response)
        {
            StatusCode = response.StatusCode;

            if (HttpStatusCode.OK.Equals(response.StatusCode))
                Data = JsonConvert.DeserializeObject<ListAnnouncement>(response.Data);
            else
                Data = null;
        }
    }
}
