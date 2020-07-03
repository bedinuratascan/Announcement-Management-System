using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnouncApp.Domain
{
    public partial class Recommendation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Announcement Announcement { get; set; }
    }
}
