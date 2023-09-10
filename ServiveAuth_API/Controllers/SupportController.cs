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
    public class SupportController : ControllerBase
    {
        private readonly IServiceSupport _serviceSupport;

        public SupportController(IServiceSupport serviceSupport)
        {
            _serviceSupport = serviceSupport;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupportTicket(SupportTicket ticket)
        {
            var createdTicket = await _serviceSupport.CreateSupportTicketAsync(ticket);
            return CreatedAtAction(nameof(GetSupportTicketById), new { id = createdTicket.Id }, createdTicket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportTicket(ObjectId id)
        {
            await _serviceSupport.DeleteSupportTicketAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSupportTickets()
        {
            var tickets = await _serviceSupport.GetAllSupportTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportTicketById(ObjectId id)
        {
            var ticket = await _serviceSupport.GetSupportTicketByIdAsync(id);
            return Ok(ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupportTicket(ObjectId id, SupportTicket ticket)
        {
            await _serviceSupport.UpdateSupportTicketAsync(id, ticket);
            return Ok();
        }
    }
}
