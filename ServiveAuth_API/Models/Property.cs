using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ServiceAuth_API.Models
{
    public class Property
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? UnitNumber { get; set; }
        public string? Size { get; set; }
        public List<string>? Amenities { get; set; }
        public ObjectId OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public ObjectId? LastModifiedBy { get; set; }
    }
}
