using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceAuth_API.Models;
using ServiceAuth_API.Services;
using System.Threading.Tasks;

namespace ServiceAuth_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("PolicyLocal")]
    [ApiController]
    public class PropietorController : ControllerBase
    {
        private readonly IServicePropietor _servicePropietor;

        public PropietorController(IServicePropietor servicePropietor)
        {
            _servicePropietor = servicePropietor;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User proprietor)
        {
            var createdProprietor = await _servicePropietor.AddAsync(proprietor);
            return CreatedAtAction(nameof(Get), new { id = createdProprietor.Id }, createdProprietor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(User proprietor, ObjectId id)
        {
            var updatedProprietor = await _servicePropietor.UpdateAsync(proprietor, id);
            return Ok(updatedProprietor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            var deletedProprietor = await _servicePropietor.DeleteAsync(id);
            return Ok(deletedProprietor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ObjectId id)
        {
            var proprietor = await _servicePropietor.GetAsync(id);
            return Ok(proprietor);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proprietors = await _servicePropietor.GetAllAsync();
            return Ok(proprietors);
        }
    }
}
