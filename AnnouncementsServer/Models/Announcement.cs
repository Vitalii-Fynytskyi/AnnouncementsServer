namespace AnnouncementsServer.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateAdded { get; set; }
    }
}
