using ServiceUser_API.Models;

namespace ServiceUser_API.Services{
    public interface IServiceTeacher{
        Task<List<User>> GetTeachersAsync();
        Task<User> GetTeacherAsync(string id);
        Task<User> CreateTeacherAsync(User teacher);
        Task UpdateTeacherAsync(string id, User teacher);
        Task DeleteTeacherAsync(string id);
    }
}