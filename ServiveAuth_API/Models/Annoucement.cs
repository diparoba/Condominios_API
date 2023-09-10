using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ServiceAuth_API.Models
{
    public class Announcement
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; } // Título del anuncio
        public string Content { get; set; } // Contenido del anuncio
        public DateTime PostedDate { get; set; } // Fecha de publicación
        public ObjectId PostedBy { get; set; } // Relación con el usuario que publica el anuncio
    }

}
