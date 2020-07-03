using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using MobAnnouncApp.Dependencies;
using MobAnnouncApp.Responses.AnnouncementResponse;
using MobAnnouncApp.Models.Announcement;

namespace MobAnnouncApp.Services
{
    public class AnnouncementService : BaseService
    {
        public async Task<ListResponse> FetchAllAsync(int offset = 0, int length = 100)
        {
            string path = "/announcement";
            object parameters = new { offset = offset, length = length };

            var response = await RequestService.GetAsync(path, parameters);
            var result = new ListResponse(response);

            if (!result.StatusCode.Equals(HttpStatusCode.OK))
            {
                string message = "Duyurular çekilemedi.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
            }

            return result;
        }

        public async Task<ShowResponse> FetchByIdAsync(int id)
        {
            string path = $"/announcement/{id}";

            var response = await RequestService.GetAsync(path, null);
            var result = new ShowResponse(response);

            if (!result.StatusCode.Equals(HttpStatusCode.OK))
            {
                string message = "Duyuru çekilemedi.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
            }

            return result;
        }

        public async Task<ShowResponse> CreateAsync(FormAnnouncement announcement)
        {
            string path = "/announcement";

            var response = await RequestService.PostAsync(path, announcement);
            var result = new ShowResponse(response);

            string message;

            if (result.StatusCode.Equals(HttpStatusCode.Created))
                message = "Duyurunuz oluşturuldu.";
            else
                if (result.StatusCode.Equals(HttpStatusCode.Unauthorized))
                    message = "Tekrar giriş yapmanız gerekmektedir.";
                else
                    message = "Duyurunuz oluşturulamadı.";

            DependencyService.Get<IMessage>().ShowShortTime(message);

            return result;
        }

        public async Task<ShowResponse> UpdateAsync(FormAnnouncement announcement)
        {
            string path = $"/announcement/{announcement.Id.Value}";

            var response = await RequestService.PutAsync(path, announcement);
            var result = new ShowResponse(response);

            string message; 
            if (result.StatusCode.Equals(HttpStatusCode.OK))
                message = "Duyurunuz güncellendi.";
            else
                if (result.StatusCode.Equals(HttpStatusCode.Unauthorized))
                    message = "Tekrar giriş yapmanız gerekmektedir.";
                else
                    message = "Duyurunuz oluşturulamadı.";

            DependencyService.Get<IMessage>().ShowShortTime(message);

            return result;
        }

        public async Task<ShowResponse> CreateOrUpdateAsync(FormAnnouncement annoncement)
        {
            if (annoncement.Id.HasValue)
                return await UpdateAsync(annoncement);
            else
                return await CreateAsync(annoncement);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string path = $"/announcement/{id}";
            var response = await RequestService.DeleteAsync(path);
            string message;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
                message = "Duyuru başarıyla silindi.";
            else if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
                message = "Bu duyuruyu silmeye yetkiniz bulunmamaktadır.";
            else
                message = "Duyuru silinemedi.";

            DependencyService.Get<IMessage>().ShowShortTime(message);

            return response.StatusCode.Equals(HttpStatusCode.OK);
        }

        public async Task<bool> LikeAsync(int id)
        {
            string path = $"/announcement/{id}/like";

            var response = await RequestService.PostAsync(path, null);

            string message;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
                message = "Duyuruyu beğendiniz.";
            else
                message = "Duyuruyu beğenemediniz";

            DependencyService.Get<IMessage>().ShowShortTime(message);

            return response.StatusCode.Equals(HttpStatusCode.OK);
        }

        public async Task<bool> UnLikeAsync(int id)
        {
            string path = $"/announcement/{id}/like";

            var response = await RequestService.DeleteAsync(path);

            string message;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
                message = "Duyurudan beğenizi çektiniz.";
            else
                message = "Duyurudan beğenizi çekemediniz.";

            DependencyService.Get<IMessage>().ShowShortTime(message);

            return response.StatusCode.Equals(HttpStatusCode.OK);
        }

        public async Task<ListResponse> SearchAsync(string text, int offset = 0, int length = 100)
        {
            string path = "/announcement/search";
            object parameters = new { text = text, offset = offset, length = length };

            var response = await RequestService.GetAsync(path, parameters);
            var result = new ListResponse(response);

            if (!result.StatusCode.Equals(HttpStatusCode.OK))
            {
                string message = "Arama sonuçları alınamadı.";
                DependencyService.Get<IMessage>().ShowShortTime(message);
            }

            return result;
        }
    }
}
