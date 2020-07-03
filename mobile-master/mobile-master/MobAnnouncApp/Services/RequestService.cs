using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MobAnnouncApp.Services
{
    public class RequestService
    {
        private const string BASE_URL = "http://89.252.131.113/api";
        private readonly HttpClient Client = new HttpClient();

        public async Task<Response> PostAsync(string path, object data, bool neeedAuth = true)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            StringContent content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(GenerateUrl(path)),
                Method = HttpMethod.Post,
                Content = content
            };

            if (neeedAuth)
            {
                request.Headers.Add("Authorization", $"Bearer {AuthorizationToken()}");
            }

            var response = await Client.SendAsync(request);
            string dataResult = response.Content.ReadAsStringAsync().Result;

            return new Response { Data = dataResult, StatusCode = response.StatusCode };
        }

        public async Task<Response> PutAsync(string path, object data, bool neeedAuth = true)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            StringContent content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(GenerateUrl(path)),
                Method = HttpMethod.Put,
                Content = content
            };

            if (neeedAuth)
            {
                request.Headers.Add("Authorization", $"Bearer {AuthorizationToken()}");
            }

            var response = await Client.SendAsync(request);
            string dataResult = response.Content.ReadAsStringAsync().Result;

            return new Response { Data = dataResult, StatusCode = response.StatusCode };
        }

        public async Task<Response> GetAsync(string path, object parameters)
        {
            UriBuilder builder = new UriBuilder(GenerateUrl(path));
            builder.Query = Helpers.ParamConverter.Object(parameters);

            var request = new HttpRequestMessage()
            {
                RequestUri = builder.Uri,
                Method = HttpMethod.Get
            };

            request.Headers.Add("Authorization", $"Bearer {AuthorizationToken()}");

            var response = await Client.SendAsync(request);
            string dataResult = response.Content.ReadAsStringAsync().Result;

            return new Response { Data = dataResult, StatusCode = response.StatusCode };
        }

        public async Task<Response> DeleteAsync(string path)
        {
            UriBuilder builder = new UriBuilder(GenerateUrl(path));

            var request = new HttpRequestMessage()
            {
                RequestUri = builder.Uri,
                Method = HttpMethod.Delete
            };

            request.Headers.Add("Authorization", $"Bearer {AuthorizationToken()}");

            var response = await Client.SendAsync(request);
            string dataResult = response.Content.ReadAsStringAsync().Result;

            return new Response { Data = dataResult, StatusCode = response.StatusCode };
        }

        private string GenerateUrl(string path)
        {
            return $"{BASE_URL}{path}";
        }

        private string AuthorizationToken()
        {
            string token = (string)Application.Current.Properties["token"];

            return token;
        }
    }

    public struct Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Data { get; set; }
    }
}
