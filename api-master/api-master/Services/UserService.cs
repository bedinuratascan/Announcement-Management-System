using AnnouncApp.Domain;
using AnnouncApp.Domain.Repositories;
using AnnouncApp.Domain.Responses;
using AnnouncApp.Domain.Services;
using AnnouncApp.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnnouncApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        
        public UserService(IUserRepository userRepository,IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public User CurrentUser(ClaimsPrincipal contextUser)
        {
            IEnumerable<Claim> claims = contextUser.Claims;
            string userId = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            User user = FindById(int.Parse(userId)).user;

            return user;
        }

        public UserResponse AddUser(User user)
        {
            try
            {
                userRepository.AddUser(user);
                unitOfWork.Complete();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı eklenirken bir hata meydana geldi::{ex.Message}");
            }
        }

        public UserResponse FindById(int userId)
        {
            try
            {
                User user = userRepository.FindById(userId);
                if (user == null)
                {
                    return new UserResponse("Kullanıcı bulunamadı.");
                }
                return new UserResponse(user);
            }
            catch (Exception ex)
            {

                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi::{ex.Message}");
            }
        }

        public User FindByEmail(string email)
        {
            return userRepository.FindByEmail(email.Trim());
        }

        public UserResponse FindEmailandPassword(string email, string password)
        {
            try
            {
                User user = userRepository.FindByEmailandPassword(email, password);
                if (user == null)
                {
                    return new UserResponse("Kullanıcı bulunamadı.");
                }
                return new UserResponse(user);

            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi::{ex.Message}");
            }
           
        }

        public UserResponse GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                User user = userRepository.GetUserWithRefreshToken(refreshToken);
                if (user == null)
                {
                    return new UserResponse("Kullanıcı bulunamadı.");
                }
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi::{ex.Message}");
            }
        }

        public void RemoveRefreshToken(User user)
        {
            try
            {
                userRepository.RemoveRefreshToken(user);
                unitOfWork.Complete();
            }
            catch (Exception)
            {

                //logging
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                userRepository.SaveRefreshToken(userId, refreshToken);
                unitOfWork.Complete();

            }
            catch (Exception)
            {

                //logging
            }
        }
    }
}
