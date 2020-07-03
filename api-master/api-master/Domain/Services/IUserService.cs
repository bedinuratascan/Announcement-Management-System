using AnnouncApp.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Services
{
    public interface IUserService
    {
        UserResponse AddUser(User user);
        UserResponse FindById(int userId);
        User FindByEmail(string email);
        UserResponse FindEmailandPassword(string email, string password);
        void SaveRefreshToken(int userId, string refreshToken);
        UserResponse GetUserWithRefreshToken(string refreshToken);
        void RemoveRefreshToken(User user);
        User CurrentUser(ClaimsPrincipal contextUser);
    }
}
