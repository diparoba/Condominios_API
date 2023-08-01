using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceAuth_API.DTO;
using ServiceAuth_API.Models;
using ServiceAuth_API.Services;

namespace ServiceAuth_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("PolicyLocal")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private readonly IServiceProperty _serviceProperty;

        public PropertyController(IServiceProperty serviceProperty)
        {
            _serviceProperty = serviceProperty;
        }
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<PropertyDTO>>> GetProperties()
        {
            var properties = await _serviceProperty.GetAllProperties();
            return Ok(properties);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyDTO>> CreateProperty(PropertyDTO propertyDTO)
        {
            if (string.IsNullOrEmpty(propertyDTO.OwnerId))
            {
                return BadRequest("Id y OwnerId no pueden ser nulos o vacíos.");
            }

            var property = new Property
            {
                Name = propertyDTO.Name,
                Address = propertyDTO.Address,
                UnitNumber = propertyDTO.UnitNumber,
                Size = propertyDTO.Size,
                Amenities = propertyDTO.Amenities,
                OwnerId = propertyDTO.OwnerId,
                Status = propertyDTO.Status
            };

            var createdProperty = await _serviceProperty.CreateProperty(property, ObjectId.Parse(propertyDTO.OwnerId));
            return Ok(createdProperty);
        }

    }
}
