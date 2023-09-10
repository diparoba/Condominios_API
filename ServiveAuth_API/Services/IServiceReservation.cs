using MongoDB.Bson;
using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public interface IServiceReservation
    {
        Task<Reservation> AddReservationAsync(Reservation reservation);
        Task<Reservation> GetReservationByIdAsync(ObjectId id);
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task UpdateReservationAsync(ObjectId id, Reservation reservation);
        Task DeleteReservationAsync(ObjectId id);
    }
}
