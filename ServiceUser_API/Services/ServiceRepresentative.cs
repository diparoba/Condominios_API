using MongoDB.Driver;
using ServiceUser_API.Models;
using ServiceUser_API.Repositories;

namespace ServiceUser_API.Services
{
    public class ServiceRepresentative : IServiceRepresentative
    {
        private readonly MongoDBRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Representative> _representatives;
        public ServiceRepresentative(IConfiguration configuration, MongoDBRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
            _representatives = _repository.database.GetCollection<Representative>("Representatives");
        }
        public async Task<List<Representative>> GetRepresentativesAsync()
        {
            return await _representatives.Find(representative => true).ToListAsync();
        }
        public async Task<Representative> GetRepresentativeAsync(string id)
        {
            return await _representatives.Find<Representative>(representative => representative.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<Representative> CreateRepresentativeAsync(Representative representative)
        {
            await _representatives.InsertOneAsync(representative);
            return representative;
        }
        public async Task UpdateRepresentativeAsync(string id, Representative representative)
        {
            await _representatives.ReplaceOneAsync(representative => representative.Id.Equals(id), representative);
        }
        public async Task DeleteRepresentativeAsync(string id)
        {
            await _representatives.DeleteOneAsync(representative => representative.Id.Equals(id));
        }
    }
}
