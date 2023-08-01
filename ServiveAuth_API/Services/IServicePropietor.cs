using MongoDB.Bson;
using ServiceAuth_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAuth_API.Services
{
    public interface IServicePropietor
    {
        Task<User> AddAsync(User proprietor);
        Task<User> UpdateAsync(User proprietor, ObjectId id);
        Task<User> DeleteAsync(ObjectId id);
        Task<User> GetAsync(ObjectId id);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(ObjectId id);
    }
}