using System.Threading.Tasks;
using MobAnnouncApp.Responses.User;
using System.Net;
using Xamarin.Forms;
using MobAnnouncApp.Dependencies;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace MobAnnouncApp.Services
{
    public class UserService : BaseService
    {
        public async Task<bool> SignUpAsync(string email, string password, string name, string surname)
        {
            object data = new { Email = email, Password = password, Name = name, SurName = surname };
            string path = "/user";

            var response = await RequestService.PostAsync(path, data, false);
            var result = new SignUpResponse(response);

            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                string message = "Hesabınız başarıyla oluşturuldu artık giriş yapabilirsiniz.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
            }
            else
            {
                string message;
                if (result.Data.HasValue)
                    message = result.Data.Value.Message;
                else
                    message = "Hesab oluşturulamadı lütfen tekrar deneyiniz.";

                DependencyService.Get<IMessage>().ShowShortTime(message);
            }

            return result.StatusCode.Equals(HttpStatusCode.OK);
        }

        public async Task<bool> SignInAsync(string email, string password)
        {
            object data = new { email = email, password = password };
            string path = "/login/AccessToken";

            var response = await RequestService.PostAsync(path, data, false);
            var result = new SignInResponse(response);

            if ( result.StatusCode.Equals(HttpStatusCode.OK))
            {
                string message = "Başarıyla giriş yaptınız.";
                DependencyService.Get<IMessage>().ShowShortTime(message);

                var auth = result.Data.Value;

                await SecureStorage.SetAsync("token", auth.Token);

                Application.Current.Properties["token"] = auth.Token;

                await FetchUserAsync();
            }
            else
            {
                string message = "Email veya parolanız yanlış.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
            }

            return result.StatusCode.Equals(HttpStatusCode.OK);
        }

        public async Task<bool> FetchUserAsync()
        {
            string path = "/user";

            var response = await RequestService.GetAsync(path, null);
            var result = new ProfileResponse(response);

            if (result.StatusCode.Equals(HttpStatusCode.OK))
            {
                Application.Current.Properties["full_name"] = result.Data.Value.FullName;

                await SecureStorage.SetAsync("full_name", result.Data.Value.FullName);
            }
            else
            {
                string message = "Profil bilgileriniz çekilemedi.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
            }

            return result.StatusCode.Equals(HttpStatusCode.OK);
        }

        public string CurrentUserName()
        {
            try
            {
                return (string)Application.Current.Properties["full_name"];
            }
            catch
            {
                return null;
            }
        }

        public async Task<AnnouncementListResponse> FetchAnnouncements(int offset = 0, int length = 100)
        {
            string path = "/user/announcements";
            object parameters = new { offset = offset, length = length };

            var response = await RequestService.GetAsync(path, parameters);
            var result = new AnnouncementListResponse(response);

            if (!result.StatusCode.Equals(HttpStatusCode.OK))
            {
                string message = "Duyurularınız çekilemedi.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
            }

            return result;
        }

        public async Task<LikeListResponse> FetchLikes(int offset = 0, int length = 100)
        {
            string path = "/user/likes";
            object parameters = new { offset = offset, length = length };

            var response = await RequestService.GetAsync(path, parameters);
            var result = new LikeListResponse(response);

            if (!result.StatusCode.Equals(HttpStatusCode.OK))
            {
                string message = "Beğenileriniz çekilemedi.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
            }

            return result;
        }

        public async Task<RecommendationListResponse> FetchRecommendations(int offset = 0, int length = 100)
        {
            string path = "/user/recommendations";
            object parameters = new { offset = offset, length = length };

            var response = await RequestService.GetAsync(path, parameters);
            var result = new RecommendationListResponse(response);

            if (!result.StatusCode.Equals(HttpStatusCode.OK))
            {
                string message = "Önerileriniz çekilemedi.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
            }

            return result;
        }
    }
}
