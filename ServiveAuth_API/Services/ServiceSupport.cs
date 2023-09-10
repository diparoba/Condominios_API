using MongoDB.Bson;
using MongoDB.Driver;
using ServiceAuth_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAuth_API.Services
{
    public class ServiceSupport : IServiceSupport
    {
        private readonly IMongoCollection<SupportTicket> _supportTickets;

        public ServiceSupport(MongoDBRepository repository) // Asumiendo que tienes un MongoDBRepository
        {
            _supportTickets = repository.database.GetCollection<SupportTicket>("SupportTickets");
        }

        public async Task<SupportTicket> CreateSupportTicketAsync(SupportTicket ticket)
        {
            await _supportTickets.InsertOneAsync(ticket);
            return ticket;
        }

        public async Task DeleteSupportTicketAsync(ObjectId id)
        {
            var result = await _supportTickets.DeleteOneAsync(t => t.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("No se encontró ningún ticket de soporte con este ID.");
            }
        }

        public async Task<IEnumerable<SupportTicket>> GetAllSupportTicketsAsync()
        {
            return await _supportTickets.Find(t => true).ToListAsync();
        }

        public async Task<SupportTicket> GetSupportTicketByIdAsync(ObjectId id)
        {
            var ticket = await _supportTickets.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (ticket == null)
            {
                throw new Exception("No se encontró ningún ticket de soporte con este ID.");
            }
            return ticket;
        }

        public async Task UpdateSupportTicketAsync(ObjectId id, SupportTicket ticket)
        {
            var result = await _supportTickets.ReplaceOneAsync(t => t.Id == id, ticket);
            if (result.ModifiedCount == 0)
            {
                throw new Exception("No se encontró ningún ticket de soporte con este ID.");
            }
        }
    }
}
