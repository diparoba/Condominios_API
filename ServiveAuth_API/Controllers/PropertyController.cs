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
            var propertiesDtos = properties.ToList().ConvertAll(p => new PropertyDTO
            {
                Id = p.Id?.ToString(),
                Name = p.Name,
                Address = p.Address,
                UnitNumber = p.UnitNumber,
                Size = p.Size,
                Amenities = p.Amenities,
                OwnerId = p.OwnerId?.ToString(),
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                LastModifiedAt = p.LastModifiedAt,
                LastModifiedBy = p.LastModifiedBy?.ToString()
            });

            return Ok(propertiesDtos);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyDTO>> CreateProperty(PropertyDTO propertyDto)
        {
            if (string.IsNullOrEmpty(propertyDto.OwnerId))
            {
                return BadRequest("OwnerId no puede ser nulo o vacío.");
            }

            ObjectId ownerId;
            ObjectId lastModifiedBy = ObjectId.Empty;
            ObjectId propertyId = ObjectId.Empty;

            try
            {
                ownerId = ObjectId.Parse(propertyDto.OwnerId);
                if (!string.IsNullOrEmpty(propertyDto.LastModifiedBy))
                {
                    lastModifiedBy = ObjectId.Parse(propertyDto.LastModifiedBy);
                }
                if (!string.IsNullOrEmpty(propertyDto.Id))
                {
                    propertyId = ObjectId.Parse(propertyDto.Id);
                }
            }
            catch
            {
                return BadRequest("Uno o más campos tienen un formato ObjectId inválido.");
            }

            var property = new Property
            {
                Id = propertyId,
                Name = propertyDto.Name,
                Address = propertyDto.Address,
                UnitNumber = propertyDto.UnitNumber,
                Size = propertyDto.Size,
                Amenities = propertyDto.Amenities,
                OwnerId = ownerId,
                Status = propertyDto.Status,
                CreatedAt = propertyDto.CreatedAt,
                LastModifiedAt = propertyDto.LastModifiedAt,
                LastModifiedBy = lastModifiedBy
            };

            var createdProperty = await _serviceProperty.CreateProperty(property, property.OwnerId);
            var createdPropertyDto = new PropertyDTO
            {
                Id = createdProperty.Id.ToString(),
                Name = createdProperty.Name,
                Address = createdProperty.Address,
                UnitNumber = createdProperty.UnitNumber,
                Size = createdProperty.Size,
                Amenities = createdProperty.Amenities,
                OwnerId = createdProperty.OwnerId.ToString(),
                Status = createdProperty.Status,
                CreatedAt = createdProperty.CreatedAt,
                LastModifiedAt = createdProperty.LastModifiedAt,
                LastModifiedBy = createdProperty.LastModifiedBy?.ToString()
            };

            return Ok(createdPropertyDto);
        }

    }
}
