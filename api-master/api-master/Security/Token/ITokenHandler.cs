using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncApp.Domain;

namespace AnnouncApp.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);

        void RevokeRefreshToken(User user);


    }
}
