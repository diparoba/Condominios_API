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
    public class ResidentController : ControllerBase
    {
        private readonly IServiceResident _serviceResident;

        public ResidentController(IServiceResident serviceResident)
        {
            _serviceResident = serviceResident;
        }

        [HttpPost]
        public async Task<IActionResult> AddResident(User resident)
        {
            var createdResident = await _serviceResident.AddAsync(resident);
            return CreatedAtAction(nameof(GetResidentById), new { id = createdResident.Id }, createdResident);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResident(ObjectId id)
        {
            var deletedResident = await _serviceResident.DeleteAsync(new User { Id = id });
            return Ok(deletedResident);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResidents()
        {
            var residents = await _serviceResident.GetAllAsync();
            return Ok(residents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResidentById(ObjectId id)
        {
            var resident = await _serviceResident.GetByIdAsync(id);
            return Ok(resident);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResident(ObjectId id, User resident)
        {
            var updatedResident = await _serviceResident.UpdateAsync(resident, id);
            return Ok(updatedResident);
        }
    }
}
