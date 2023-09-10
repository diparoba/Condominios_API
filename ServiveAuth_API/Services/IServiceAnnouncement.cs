using MongoDB.Bson;
using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public interface IServiceAnnouncement
    {
        Task<Announcement> AddAnnouncementAsync(Announcement announcement);
        Task<Announcement> GetAnnouncementByIdAsync(ObjectId id);
        Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync();
        Task UpdateAnnouncementAsync(ObjectId id, Announcement announcement);
        Task DeleteAnnouncementAsync(ObjectId id);
    }
}
