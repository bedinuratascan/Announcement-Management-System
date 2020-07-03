namespace AnnouncApp.Domain
{
    public partial class Like
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Announcement Announcement { get; set; }
    }
}