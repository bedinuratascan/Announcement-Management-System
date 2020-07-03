using System;
namespace MobAnnouncApp.Models.Announcement
{
    public struct FormAnnouncement
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
    }
}
