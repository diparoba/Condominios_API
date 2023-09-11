namespace ServiceAuth_API.DTO
{
    public class AnnouncementDTO
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime PostedDate { get; set; }
        public string? PostedBy { get; set; }
    }
}
