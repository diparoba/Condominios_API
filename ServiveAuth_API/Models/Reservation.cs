using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ServiceAuth_API.Models
{
    public class Reservation
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; } // Relación con el usuario que realiza la reserva
        public string Space { get; set; } // Espacio reservado (salón de eventos, cancha deportiva, etc.)
        public DateTime StartDate { get; set; } // Fecha y hora de inicio de la reserva
        public DateTime EndDate { get; set; } // Fecha y hora de fin de la reserva
        public string Status { get; set; } // Estado de la reserva (Pendiente, Confirmada, Cancelada, etc.)
    }
}
