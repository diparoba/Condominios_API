using MongoDB.Driver;
using ServiceUser_API.Models;
using ServiceUser_API.Repositories;

namespace ServiceUser_API.Services
{
    public class ServiceStudent : IServiceStudent
    {
        private readonly MongoDBRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<User> _users;

        public ServiceStudent(IConfiguration configuration, MongoDBRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
            _users = _repository.database.GetCollection<User>("Users");
        }
        public Task<List<User>> GetStudentsAsync()
        {
            throw new NotImplementedException();
        }
        public Task<User> GetStudentAsync(string id)
        {
            throw new NotImplementedException();
        }
        public async Task<User> CreateStudentAsync(User student)
        {
            var userExists = await _users.Find(u => u.Email.Equals(student.Email)).AnyAsync();
            if (userExists)
            {
                throw new Exception("User already exists");
            }
            student.Status = Status.Active;
            student.Roles = new List<UserRole> { UserRole.Student };
            return student;
        }
        public Task UpdateStudentAsync(string id, User student)
        {
            throw new NotImplementedException();
        }
        public Task DeleteStudentAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}