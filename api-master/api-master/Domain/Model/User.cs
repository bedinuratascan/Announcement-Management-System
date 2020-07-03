using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnouncApp.Domain
{
    public partial class User
    {
        public int Id { get; set; }
        [RegularExpression(@"karabuk\.edu\.tr$")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string RefreshToken { get; set; }
        public List<Like> Likes { get; set; }
        public List<Announcement> Announcements { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public List<Interaction> Interactions { get; set; }
        public List<Recommendation> Recommendations { get; set; }
    }
}
