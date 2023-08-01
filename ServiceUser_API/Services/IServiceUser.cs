using ServiceUser_API.Models;

namespace ServiceUser_API.Services
{
    public interface IServiceUser{
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(string id);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(string id, User user);
        Task DeleteUserAsync(string id);
    }
}