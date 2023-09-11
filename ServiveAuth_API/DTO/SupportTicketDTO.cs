namespace ServiceAuth_API.DTO
{
    public class SupportTicketDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Space { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
