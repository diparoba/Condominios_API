using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceAuth_API.Models;
using ServiceAuth_API.DTO;
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
        public async Task<IActionResult> Create(UserDTO proprietorDto)
        {
            var proprietor = new User
            {
                Id = ObjectId.Parse(proprietorDto.Id),
                Name = proprietorDto.Name,
                Lastname = proprietorDto.Lastname,
                Email = proprietorDto.Email,
                Password = proprietorDto.Password,
                Status = proprietorDto.Status,
                Identificacion = proprietorDto.Identificacion,
                Roles = proprietorDto.Roles,
                CreatedAt = proprietorDto.CreatedAt,
                LastModifiedAt = proprietorDto.LastModifiedAt,
                LastModifiedBy = ObjectId.Parse(proprietorDto.LastModifiedBy)
            };

            var createdProprietor = await _servicePropietor.AddAsync(proprietor);
            var createdProprietorDto = new UserDTO
            {
                Id = createdProprietor.Id.ToString(),
                Name = createdProprietor.Name,
                Lastname = createdProprietor.Lastname,
                Email = createdProprietor.Email,
                Password = createdProprietor.Password,
                Status = createdProprietor.Status,
                Identificacion = createdProprietor.Identificacion,
                Roles = createdProprietor.Roles,
                CreatedAt = createdProprietor.CreatedAt,
                LastModifiedAt = createdProprietor.LastModifiedAt,
                LastModifiedBy = createdProprietor.LastModifiedBy?.ToString()
            };

            return CreatedAtAction(nameof(Get), new { id = createdProprietorDto.Id }, createdProprietorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UserDTO proprietorDto, string id)
        {
            var proprietor = new User
            {
                Id = ObjectId.Parse(proprietorDto.Id),
                Name = proprietorDto.Name,
                Lastname = proprietorDto.Lastname,
                Email = proprietorDto.Email,
                Password = proprietorDto.Password,
                Status = proprietorDto.Status,
                Identificacion = proprietorDto.Identificacion,
                Roles = proprietorDto.Roles,
                CreatedAt = proprietorDto.CreatedAt,
                LastModifiedAt = proprietorDto.LastModifiedAt,
                LastModifiedBy = ObjectId.Parse(proprietorDto.LastModifiedBy)
            };

            var updatedProprietor = await _servicePropietor.UpdateAsync(proprietor, ObjectId.Parse(id));
            var updatedProprietorDto = new UserDTO
            {
                Id = updatedProprietor.Id.ToString(),
                Name = updatedProprietor.Name,
                Lastname = updatedProprietor.Lastname,
                Email = updatedProprietor.Email,
                Password = updatedProprietor.Password,
                Status = updatedProprietor.Status,
                Identificacion = updatedProprietor.Identificacion,
                Roles = updatedProprietor.Roles,
                CreatedAt = updatedProprietor.CreatedAt,
                LastModifiedAt = updatedProprietor.LastModifiedAt,
                LastModifiedBy = updatedProprietor.LastModifiedBy?.ToString()
            };

            return Ok(updatedProprietorDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deletedProprietor = await _servicePropietor.DeleteAsync(ObjectId.Parse(id));
            return Ok(deletedProprietor); // Aquí podrías devolver el DTO si es necesario
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var proprietor = await _servicePropietor.GetAsync(ObjectId.Parse(id));
            var proprietorDto = new UserDTO
            {
                Id = proprietor.Id.ToString(),
                Name = proprietor.Name,
                Lastname = proprietor.Lastname,
                Email = proprietor.Email,
                Password = proprietor.Password,
                Status = proprietor.Status,
                Identificacion = proprietor.Identificacion,
                Roles = proprietor.Roles,
                CreatedAt = proprietor.CreatedAt,
                LastModifiedAt = proprietor.LastModifiedAt,
                LastModifiedBy = proprietor.LastModifiedBy?.ToString()
            };

            return Ok(proprietorDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proprietors = await _servicePropietor.GetAllAsync();
            var proprietorsDtos = proprietors.ConvertAll(p => new UserDTO
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Lastname = p.Lastname,
                Email = p.Email,
                Password = p.Password,
                Status = p.Status,
                Identificacion = p.Identificacion,
                Roles = p.Roles,
                CreatedAt = p.CreatedAt,
                LastModifiedAt = p.LastModifiedAt,
                LastModifiedBy = p.LastModifiedBy?.ToString()
            });

            return Ok(proprietorsDtos);
        }
    }
}
