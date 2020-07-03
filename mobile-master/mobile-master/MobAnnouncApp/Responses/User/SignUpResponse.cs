using System;
using System.Net;
using MobAnnouncApp.Services;
using Newtonsoft.Json;

namespace MobAnnouncApp.Responses.User
{
    public struct SignUpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public SignUpData? Data { get; set; }

        public SignUpResponse(Response response)
        {
            StatusCode = response.StatusCode;

            if (HttpStatusCode.OK.Equals(response.StatusCode))
                Data = null;
            else
                Data = JsonConvert.DeserializeObject<SignUpData>(response.Data);
        }
    }

    public struct SignUpData
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
