using MongoDB.Bson;
using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public interface IServiceDocument
    {
        Task<Document> UploadDocumentAsync(Document document);
        Task<Document> GetDocumentByIdAsync(ObjectId id);
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task UpdateDocumentAsync(ObjectId id, Document document);
        Task DeleteDocumentAsync(ObjectId id);
    }
}
