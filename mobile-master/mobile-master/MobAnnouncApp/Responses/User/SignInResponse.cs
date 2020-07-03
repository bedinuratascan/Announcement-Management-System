using System;
using System.Net;
using MobAnnouncApp.Services;
using Newtonsoft.Json;

namespace MobAnnouncApp.Responses.User
{
    public struct SignInResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public SignInData? Data { get; set; }

        public SignInResponse(Response response)
        {
            StatusCode = response.StatusCode;

            if (HttpStatusCode.OK.Equals(response.StatusCode))
                Data = JsonConvert.DeserializeObject<SignInData>(response.Data);
            else
                Data = null;
        }
    }

    public struct SignInData
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("expiration", ItemConverterType = typeof(Helpers.JsonDateTimeConverter))]
        public DateTime ExpireAt { get; set; }
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
