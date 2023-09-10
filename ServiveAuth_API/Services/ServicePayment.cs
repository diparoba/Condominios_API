using MongoDB.Bson;
using MongoDB.Driver;
using ServiceAuth_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAuth_API.Services
{
    public class ServicePayment : IServicePayment
    {
        private readonly IMongoCollection<Payment> _payments;

        public ServicePayment(MongoDBRepository repository) // Asumiendo que tienes un MongoDBRepository
        {
            _payments = repository.database.GetCollection<Payment>("Payments");
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            await _payments.InsertOneAsync(payment);
            return payment;
        }

        public async Task DeletePaymentAsync(ObjectId id)
        {
            var result = await _payments.DeleteOneAsync(p => p.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("No se encontró ningún pago con este ID.");
            }
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _payments.Find(p => true).ToListAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(ObjectId id)
        {
            var payment = await _payments.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (payment == null)
            {
                throw new Exception("No se encontró ningún pago con este ID.");
            }
            return payment;
        }

        public async Task UpdatePaymentAsync(ObjectId id, Payment payment)
        {
            var result = await _payments.ReplaceOneAsync(p => p.Id == id, payment);
            if (result.ModifiedCount == 0)
            {
                throw new Exception("No se encontró ningún pago con este ID.");
            }
        }
    }
}
