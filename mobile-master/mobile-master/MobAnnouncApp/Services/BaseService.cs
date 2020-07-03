using System;
namespace MobAnnouncApp.Services
{
    public class BaseService
    {
        protected RequestService RequestService { get; set; }

        public BaseService()
        {
            RequestService = new RequestService();
        }
    }
}
