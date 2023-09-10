using MongoDB.Bson;
using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public interface IServicePayment
    {
        Task<Payment> AddPaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(ObjectId id);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task UpdatePaymentAsync(ObjectId id, Payment payment);
        Task DeletePaymentAsync(ObjectId id);
    }
}
