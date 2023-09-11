using MongoDB.Bson;
using MongoDB.Driver;
using ServiceAuth_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAuth_API.Services
{
    public class ServiceAnnouncement : IServiceAnnouncement
    {
        private readonly MongoDBRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Announcement> _announcements;

        public ServiceAnnouncement(IConfiguration configuration, MongoDBRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
            _announcements = _repository.database.GetCollection<Announcement>("Announcements");
        }

        public async Task<Announcement> AddAnnouncementAsync(Announcement announcement)
        {
            await _announcements.InsertOneAsync(announcement);
            return announcement;
        }

        public async Task DeleteAnnouncementAsync(ObjectId id)
        {
            var result = await _announcements.DeleteOneAsync(a => a.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("No se encontró ningún anuncio con este ID.");
            }
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync()
        {
            return await _announcements.Find(a => true).ToListAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(ObjectId id)
        {
            var announcement = await _announcements.Find(a => a.Id == id).FirstOrDefaultAsync();
            if (announcement == null)
            {
                throw new Exception("No se encontró ningún anuncio con este ID.");
            }
            return announcement;
        }

        public async Task UpdateAnnouncementAsync(ObjectId id, Announcement announcement)
        {
            var result = await _announcements.ReplaceOneAsync(a => a.Id == id, announcement);
            if (result.ModifiedCount == 0)
            {
                throw new Exception("No se encontró ningún anuncio con este ID.");
            }
        }
    }
}
