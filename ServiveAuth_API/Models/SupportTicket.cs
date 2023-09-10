using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ServiceAuth_API.Models
{
    public class SupportTicket
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; } // Relación con el usuario que solicita soporte
        public string Subject { get; set; } // Asunto del ticket
        public string Description { get; set; } // Descripción del problema o consulta
        public DateTime CreatedDate { get; set; } // Fecha de creación del ticket
        public string Status { get; set; } // Estado del ticket (Pendiente, Resuelto, etc.)
    }

}
