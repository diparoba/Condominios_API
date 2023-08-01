using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceUser_API.Models
{
    public class Representative
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityDocument { get; set; }
        public string? Relationship { get; set; } // Parentesco con el estudiante
        public Address Address { get; set; }
    }
}