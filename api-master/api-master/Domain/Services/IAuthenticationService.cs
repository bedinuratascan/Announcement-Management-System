using AnnouncApp.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Services
{
    public interface IAuthenticationService
    {
        AccessTokenResponse CreateAccessToken(string email,string password);
        AccessTokenResponse CerateAccessTokenByRefreshToken(string refreshToken);
        AccessTokenResponse RevokeRefreshToken(string refreshToken);
    }
}
