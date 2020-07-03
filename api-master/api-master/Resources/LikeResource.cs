using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AnnouncApp.Domain;

namespace AnnouncApp.Resources
{
    public class LikeResource
    {
        [Required]
        public User User { get; set; }
        [Required]
        public Announcement Announcement { get; set; }
    }
}