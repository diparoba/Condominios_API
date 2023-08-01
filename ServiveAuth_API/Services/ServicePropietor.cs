using MongoDB.Bson;
using MongoDB.Driver;
using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public class ServicePropietor : IServicePropietor
    {
        private readonly MongoDBRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<User> _users;

        public ServicePropietor(IConfiguration configuration, MongoDBRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
            _users = _repository.database.GetCollection<User>("Users");
        }
        public async Task<User> AddAsync(User proprietor)
        {
            var existingUser = await _users.Find(user => user.Identificacion == proprietor.Identificacion).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new Exception("Un usuario con esta identificación ya existe.");
            }
            if (proprietor.Roles == null)
            {
                proprietor.Roles = new List<string>();
            }
            if (!proprietor.Roles.Contains("Propietario"))
            {
                proprietor.Roles.Add("Propietario");
            }
            proprietor.CreatedAt = DateTime.UtcNow;
            proprietor.Status = "A";
            await _users.InsertOneAsync(proprietor);
            return proprietor;
        }

        public async Task<User> DeleteAsync(ObjectId id)
        {
            var update = Builders<User>.Update
                .Set(user => user.Status, "I")
                .Set(user => user.LastModifiedAt, DateTime.UtcNow);
            var result = await _users.UpdateOneAsync(user => user.Id == id, update);
            if (result.ModifiedCount == 0)
            {
                throw new Exception("No se encontró ningún usuario con este ID.");
            }
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            var proprietors = await _users.Find(user => user.Roles.Contains("Propietario") && user.Status == "A").ToListAsync();
            return proprietors;
        }
        public async Task<User> GetAsync(ObjectId id)
        {
            var proprietor = await _users.Find(user => user.Id == id && user.Roles.Contains("Propietario") && user.Status == "A").FirstOrDefaultAsync();
            if (proprietor == null)
            {
                throw new Exception("No se encontró ningún usuario con este ID que sea propietario y esté activo.");
            }
            return proprietor;
        }

        public async Task<User> GetByIdAsync(ObjectId id)
        {
            var proprietor = await _users.Find(user => user.Id == id && user.Roles.Contains("Propietario") && user.Status == "A").FirstOrDefaultAsync();
            if (proprietor == null)
            {
                throw new Exception("No se encontró ningún usuario con este ID que sea propietario y esté activo.");
            }
            return proprietor;
        }

        public async Task<User> UpdateAsync(User proprietor , ObjectId id)
        {
            // Crear una definición de actualización para los campos que quieres cambiar
            var update = Builders<User>.Update
                .Set(user => user.Name, proprietor.Name)
                .Set(user => user.Lastname, proprietor.Lastname)
                .Set(user => user.Email, proprietor.Email)
                .Set(user => user.Password, proprietor.Password)
                .Set(user => user.Identificacion, proprietor.Identificacion)
                .Set(user => user.Roles, proprietor.Roles)
                .Set(user => user.LastModifiedAt, DateTime.UtcNow)
                .Set(user => user.LastModifiedBy, id);  // Asegúrate de que esto sea el ID del usuario logueado

            // Realizar la actualización
            var result = await _users.UpdateOneAsync(user => user.Id == id, update);

            // Si no se actualizó ningún documento, lanzar una excepción
            if (result.ModifiedCount == 0)
            {
                throw new Exception("No se encontró ningún usuario con este ID.");
            }

            // Devolver el usuario actualizado
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }
    }
}
