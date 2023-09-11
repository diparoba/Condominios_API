using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceAuth_API.DTO;
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
        public async Task<IActionResult> UploadDocument(DocumentDTO documentDto)
        {
            var document = new Document
            {
                Id = ObjectId.Parse(documentDto.Id),
                DocumentName = documentDto.DocumentName,
                FileContent = documentDto.FileContent,
                UploadedDate = documentDto.UploadedDate,
                UploadedBy = ObjectId.Parse(documentDto.UploadedBy)
            };

            var uploadedDocument = await _serviceDocument.UploadDocumentAsync(document);
            var uploadedDocumentDto = new DocumentDTO
            {
                Id = uploadedDocument.Id.ToString(),
                DocumentName = uploadedDocument.DocumentName,
                FileContent = uploadedDocument.FileContent,
                UploadedDate = uploadedDocument.UploadedDate,
                UploadedBy = uploadedDocument.UploadedBy.ToString()
            };

            return CreatedAtAction(nameof(GetDocumentById), new { id = uploadedDocumentDto.Id }, uploadedDocumentDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(string id)
        {
            await _serviceDocument.DeleteDocumentAsync(ObjectId.Parse(id));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _serviceDocument.GetAllDocumentsAsync();
            var documentDtos = documents.ToList().ConvertAll(d => new DocumentDTO
            {
                Id = d.Id.ToString(),
                DocumentName = d.DocumentName,
                FileContent = d.FileContent,
                UploadedDate = d.UploadedDate,
                UploadedBy = d.UploadedBy.ToString()
            });

            return Ok(documentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(string id)
        {
            var document = await _serviceDocument.GetDocumentByIdAsync(ObjectId.Parse(id));
            var documentDto = new DocumentDTO
            {
                Id = document.Id.ToString(),
                DocumentName = document.DocumentName,
                FileContent = document.FileContent,
                UploadedDate = document.UploadedDate,
                UploadedBy = document.UploadedBy.ToString()
            };

            return Ok(documentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(string id, DocumentDTO documentDto)
        {
            var document = new Document
            {
                Id = ObjectId.Parse(documentDto.Id),
                DocumentName = documentDto.DocumentName,
                FileContent = documentDto.FileContent,
                UploadedDate = documentDto.UploadedDate,
                UploadedBy = ObjectId.Parse(documentDto.UploadedBy)
            };

            await _serviceDocument.UpdateDocumentAsync(ObjectId.Parse(id), document);
            return Ok();
        }
    }
}
