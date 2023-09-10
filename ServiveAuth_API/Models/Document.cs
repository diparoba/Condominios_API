using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ServiceAuth_API.Models
{
    public class Document
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string DocumentName { get; set; } // Nombre del documento
        public byte[] FileContent { get; set; } // Contenido del archivo en formato binario
        public DateTime UploadedDate { get; set; } // Fecha de subida
        public ObjectId UploadedBy { get; set; } // Relación con el usuario que sube el documento
    }

}
