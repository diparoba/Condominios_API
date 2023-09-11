namespace ServiceAuth_API.DTO
{
    public class PaymentDTO
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Description { get; set; }
    }
}
