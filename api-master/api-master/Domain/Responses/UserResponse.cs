using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Responses
{
    public class UserResponse:BaseResponse
    {
        public User user { get; set; }
        private UserResponse(bool success, string message, User user):base(success,message)
        {
            this.user = user;
        }

        //başarılı olursa;
        public UserResponse(User user):this(true,string.Empty,user){ }
        //başarısız olursa;
        public UserResponse(string message) : this(false, message, null){ }
    }
}
