using AnnouncApp.Domain;
using AnnouncApp.Resources;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Mapping
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<UserResource, User>();
            CreateMap<User, UserResource>();
        }
    }
}
