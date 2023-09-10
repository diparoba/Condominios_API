using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public interface IServiceResident
    {
        Task<User> AddAsync(User resident);
        Task<User> UpdateAsync(User resident, Object id);
        Task<User> DeleteAsync(User resident);
        Task<User> GetAsync(User resident);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(Object id);
    }
}
