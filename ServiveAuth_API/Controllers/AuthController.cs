using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceAuth_API.Models;
using ServiceAuth_API.Services;
using System.Threading.Tasks;
using ServiceAuth_API.DTO;

namespace ServiceAuth_API.Controllers
{
    [ApiController]
    [EnableCors("PolicyLocal")]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceAuth _serviceAuth;

        public AuthController(IServiceAuth serviceAuth)
        {
            _serviceAuth = serviceAuth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var user = new User
            {
                Id = userDTO.Id != null ? ObjectId.Parse(userDTO.Id) : ObjectId.Empty,
                Name = userDTO.Name,
                Lastname = userDTO.Lastname,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Status = userDTO.Status,
                Identificacion = userDTO.Identificacion,
                Roles = userDTO.Roles,
                CreatedAt = userDTO.CreatedAt,
                LastModifiedAt = userDTO.LastModifiedAt,
                LastModifiedBy = userDTO.LastModifiedBy != null ? ObjectId.Parse(userDTO.LastModifiedBy) : (ObjectId?)null
            };

            var result = await _serviceAuth.RegisterAsync(user);
            if (result == null)
            {
                return BadRequest("Username already exists");
            }

            var resultDTO = new UserDTO
            {
                Id = result.Id.ToString(),
                Name = result.Name,
                Lastname = result.Lastname,
                Email = result.Email,
                Password = result.Password,
                Status = result.Status,
                Identificacion = result.Identificacion,
                Roles = result.Roles,
                CreatedAt = result.CreatedAt,
                LastModifiedAt = result.LastModifiedAt,
                LastModifiedBy = result.LastModifiedBy?.ToString()
            };

            return Ok(resultDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var user = new User
            {
            
                Email = userDTO.Email,
                Password = userDTO.Password
    
            };

            var result = await _serviceAuth.LoginAsync(user);
            if (result == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok(new
            {
                token = _serviceAuth.GenerateToken(result),
                UserId = result.Id.ToString(),
                Name = result.Name,
                Lastname = result.Lastname,
                Email = result.Email,
                Roles = result.Roles
            });
        }
    }
}
