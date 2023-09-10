using MongoDB.Bson;
using MongoDB.Driver;
using ServiceAuth_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAuth_API.Services
{
    public class ServiceDocument : IServiceDocument
    {
        private readonly IMongoCollection<Document> _documents;

        public ServiceDocument(MongoDBRepository repository) // Asumiendo que tienes un MongoDBRepository
        {
            _documents = repository.database.GetCollection<Document>("Documents");
        }

        public async Task DeleteDocumentAsync(ObjectId id)
        {
            var result = await _documents.DeleteOneAsync(d => d.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("No se encontró ningún documento con este ID.");
            }
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _documents.Find(d => true).ToListAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(ObjectId id)
        {
            var document = await _documents.Find(d => d.Id == id).FirstOrDefaultAsync();
            if (document == null)
            {
                throw new Exception("No se encontró ningún documento con este ID.");
            }
            return document;
        }

        public async Task UpdateDocumentAsync(ObjectId id, Document document)
        {
            var result = await _documents.ReplaceOneAsync(d => d.Id == id, document);
            if (result.ModifiedCount == 0)
            {
                throw new Exception("No se encontró ningún documento con este ID.");
            }
        }

        public async Task<Document> UploadDocumentAsync(Document document)
        {
            await _documents.InsertOneAsync(document);
            return document;
        }
    }
}
