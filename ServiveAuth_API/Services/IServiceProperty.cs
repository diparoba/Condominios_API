using MongoDB.Bson;
using ServiceAuth_API.DTO;
using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public interface IServiceProperty
    {
        Task<IEnumerable<PropertyDTO>> GetAllProperties();
        Task<Property> GetPropertyById(string id);
        Task<Property> CreateProperty(Property property, ObjectId id);
        Task UpdateProperty(string id, Property propertyIn);
        Task RemoveProperty(Property propertyIn);
        Task RemovePropertyById(string id);
    }
}
