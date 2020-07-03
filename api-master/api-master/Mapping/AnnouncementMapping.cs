using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncApp.Domain;
using AnnouncApp.Resources;
using AutoMapper;

namespace AnnouncApp.Mapping
{
    public class AnnouncementMapping:Profile
    {

        public AnnouncementMapping()
        {
            CreateMap<AnnouncementResource, Announcement>();
            CreateMap<Announcement, AnnouncementResource>();
        }

    }
}
