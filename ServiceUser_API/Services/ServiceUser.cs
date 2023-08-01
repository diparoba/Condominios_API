using MongoDB.Driver;
using ServiceUser_API.Models;
using ServiceUser_API.Repositories;

namespace ServiceUser_API.Services
{
    public class ServiceUser : IServiceUser
    {
        private readonly MongoDBRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<User> _users;

        public ServiceUser(IConfiguration configuration, MongoDBRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
            _users = _repository.database.GetCollection<User>("Users");
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }
        public async Task<User> GetUserAsync(string id)
        {
            return await _users.Find<User>(user => user.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<User> CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }
        public async Task UpdateUserAsync(string id, User user)
        {
            await _users.ReplaceOneAsync(user => user.Id.Equals(id), user);
        }
        public async Task DeleteUserAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.Id.Equals(id));
        }
    }
}