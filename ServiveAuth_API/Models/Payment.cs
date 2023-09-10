using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ServiceAuth_API.Models
{
    public class Payment
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; } // Relación con el usuario que realiza el pago
        public decimal Amount { get; set; } // Monto del pago
        public DateTime PaymentDate { get; set; } // Fecha del pago
        public string PaymentMethod { get; set; } // Método de pago (Efectivo, Transferencia, etc.)
        public string Description { get; set; } // Descripción o concepto del pago
    }

}
