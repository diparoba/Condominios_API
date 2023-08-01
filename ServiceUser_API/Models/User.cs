using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceUser_API.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public Status? Status { get; set; }
        public List<UserRole>? Roles { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityDocument { get; set; }
        public ObjectId? LegalRepresentativeId { get; set; }
        public ObjectId? EconomicRepresentativeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? LastModifiedBy { get; set; }
    }
}