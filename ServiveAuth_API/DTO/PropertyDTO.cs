

namespace ServiceAuth_API.DTO
{
    public class PropertyDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? UnitNumber { get; set; }
        public string? Size { get; set; }
        public List<string>? Amenities { get; set; }
        public string? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }

    }

}
