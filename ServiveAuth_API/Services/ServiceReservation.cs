using MongoDB.Bson;
using MongoDB.Driver;
using ServiceAuth_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAuth_API.Services
{
    public class ServiceReservation : IServiceReservation
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public ServiceReservation(MongoDBRepository repository) // Asumiendo que tienes un MongoDBRepository
        {
            _reservations = repository.database.GetCollection<Reservation>("Reservations");
        }

        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            await _reservations.InsertOneAsync(reservation);
            return reservation;
        }

        public async Task DeleteReservationAsync(ObjectId id)
        {
            var result = await _reservations.DeleteOneAsync(r => r.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("No se encontró ninguna reserva con este ID.");
            }
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _reservations.Find(r => true).ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(ObjectId id)
        {
            var reservation = await _reservations.Find(r => r.Id == id).FirstOrDefaultAsync();
            if (reservation == null)
            {
                throw new Exception("No se encontró ninguna reserva con este ID.");
            }
            return reservation;
        }

        public async Task UpdateReservationAsync(ObjectId id, Reservation reservation)
        {
            var result = await _reservations.ReplaceOneAsync(r => r.Id == id, reservation);
            if (result.ModifiedCount == 0)
            {
                throw new Exception("No se encontró ninguna reserva con este ID.");
            }
        }
    }
}
