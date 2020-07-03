using AnnouncApp.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Responses
{
    public class AccessTokenResponse:BaseResponse
    {
        public AccessToken accesstoken { get; set; }
        private AccessTokenResponse(bool success,string message,AccessToken accesstoken):base(success,message)
        {
            this.accesstoken = accesstoken;
        }
        //Eğer başarılı ise;
        public AccessTokenResponse(AccessToken accessToken):this(true,string.Empty,accessToken)
        {

        }
        //Eğer başarısız ise;
        public AccessTokenResponse(string message):this(false,message,null)
        {

        }
    }
}
