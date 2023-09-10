using MongoDB.Bson;
using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public interface IServiceSupport
    {
        Task<SupportTicket> CreateSupportTicketAsync(SupportTicket ticket);
        Task<SupportTicket> GetSupportTicketByIdAsync(ObjectId id);
        Task<IEnumerable<SupportTicket>> GetAllSupportTicketsAsync();
        Task UpdateSupportTicketAsync(ObjectId id, SupportTicket ticket);
        Task DeleteSupportTicketAsync(ObjectId id);
    }
}
