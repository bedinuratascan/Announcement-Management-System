namespace AnnouncApp.Domain
{
    public partial class Interaction
    {
        public enum InteractionType
        {
            VIEW,
            LIKE
        }

        public int Id { get; set; }
        public User User { get; set; }
        public Announcement Announcement { get; set; }
        public InteractionType Type { get; set; }
    }
}
