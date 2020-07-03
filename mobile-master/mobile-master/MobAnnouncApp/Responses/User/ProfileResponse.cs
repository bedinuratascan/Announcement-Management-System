using System.Net;
using MobAnnouncApp.Services;
using Newtonsoft.Json;

namespace MobAnnouncApp.Responses.User
{
    public struct ProfileResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public ProfileData? Data { get; set; }

        public ProfileResponse(Response response)
        {
            StatusCode = response.StatusCode;

            if (HttpStatusCode.OK.Equals(response.StatusCode))
                Data = JsonConvert.DeserializeObject<ProfileData>(response.Data);
            else
                Data = null;
        }
    }

    public struct ProfileData
    {
        [JsonProperty("name")]
        private string Name { get; set; }

        [JsonProperty("surName")]
        private string Surname { get; set; }

        [JsonIgnore]
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
    }
}
