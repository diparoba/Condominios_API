using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceAuth_API.Models;
using ServiceAuth_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAuth_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("PolicyLocal")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IServiceDocument _serviceDocument;

        public DocumentController(IServiceDocument serviceDocument)
        {
            _serviceDocument = serviceDocument;
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocument(Document document)
        {
            var uploadedDocument = await _serviceDocument.UploadDocumentAsync(document);
            return CreatedAtAction(nameof(GetDocumentById), new { id = uploadedDocument.Id }, uploadedDocument);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(ObjectId id)
        {
            await _serviceDocument.DeleteDocumentAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _serviceDocument.GetAllDocumentsAsync();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(ObjectId id)
        {
            var document = await _serviceDocument.GetDocumentByIdAsync(id);
            return Ok(document);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(ObjectId id, Document document)
        {
            await _serviceDocument.UpdateDocumentAsync(id, document);
            return Ok();
        }
    }
}
