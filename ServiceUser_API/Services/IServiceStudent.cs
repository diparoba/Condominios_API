using ServiceUser_API.Models;

namespace ServiceUser_API.Services
{
    public interface IServiceStudent
    {
        Task<List<User>> GetStudentsAsync();
        Task<User> GetStudentAsync(string id);
        Task<User> CreateStudentAsync(User student);
        Task UpdateStudentAsync(string id, User student);
        Task DeleteStudentAsync(string id);
    }
}