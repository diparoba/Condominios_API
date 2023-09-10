using MongoDB.Bson;
using MongoDB.Driver;
using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public class ServiceResident : IServiceResident
    {
        private readonly MongoDBRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<User> _users;

        public ServiceResident(IConfiguration configuration, MongoDBRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
            _users = _repository.database.GetCollection<User>("Users");
        }

        public async Task<User> AddAsync(User resident)
        {
            // Verificar si ya existe un usuario con la misma identificación
            var existingUser = await _users.Find(user => user.Identificacion == resident.Identificacion).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new Exception("Un usuario con esta identificación ya existe.");
            }

            // Asignar el rol de "Residente" si no está ya asignado
            if (resident.Roles == null)
            {
                resident.Roles = new List<string>();
            }
            if (!resident.Roles.Contains("Residente"))
            {
                resident.Roles.Add("Residente");
            }

            // Establecer otros campos
            resident.CreatedAt = DateTime.UtcNow;
            resident.Status = "A";

            // Insertar el nuevo residente en la base de datos
            await _users.InsertOneAsync(resident);

            return resident;
        }

        public async Task<User> DeleteAsync(User resident)
        {
            // Crear una definición de actualización para cambiar el estado y la fecha de la última modificación
            var update = Builders<User>.Update
                .Set(user => user.Status, "I")
                .Set(user => user.LastModifiedAt, DateTime.UtcNow);

            // Realizar la actualización
            var result = await _users.UpdateOneAsync(user => user.Id == resident.Id, update);

            // Si no se actualizó ningún documento, lanzar una excepción
            if (result.ModifiedCount == 0)
            {
                throw new Exception("No se encontró ningún usuario con este ID.");
            }

            // Devolver el usuario actualizado (ahora inactivo)
            return await _users.Find(user => user.Id == resident.Id).FirstOrDefaultAsync();
        }


        public async Task<List<User>> GetAllAsync()
        {
            // Buscar todos los usuarios que son residentes y están activos
            var residents = await _users.Find(user => user.Roles.Contains("Residente") && user.Status == "A").ToListAsync();

            // Si no se encontraron residentes, podrías lanzar una excepción o simplemente devolver una lista vacía
            if (residents.Count == 0)
            {
                // Opción 1: Lanzar una excepción
                // throw new Exception("No se encontraron residentes activos.");

                // Opción 2: Devolver una lista vacía
                return new List<User>();
            }

            return residents;
        }


        public async Task<User> GetAsync(User resident)
        {
            // Buscar al usuario por su ID y verificar si es un residente activo
            var existingUser = await _users.Find(user => user.Id == resident.Id && user.Roles.Contains("Residente") && user.Status == "A").FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new Exception("No se encontró ningún residente activo con este ID.");
            }

            return existingUser;
        }


        public async Task<User> GetByIdAsync(object id)
        {
            // Convertir el objeto id a ObjectId si es necesario
            ObjectId objectId;
            if (id is string idString)
            {
                objectId = ObjectId.Parse(idString);
            }
            else if (id is ObjectId)
            {
                objectId = (ObjectId)id;
            }
            else
            {
                throw new ArgumentException("El tipo de ID proporcionado no es válido.");
            }

            // Buscar al usuario por su ID y verificar si es un residente activo
            var existingUser = await _users.Find(user => user.Id == objectId && user.Roles.Contains("Residente") && user.Status == "A").FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new Exception("No se encontró ningún residente activo con este ID.");
            }

            return existingUser;
        }


        public async Task<User> UpdateAsync(User resident, object id)
        {
            // Convertir el objeto id a ObjectId si es necesario
            ObjectId objectId;
            if (id is string idString)
            {
                objectId = ObjectId.Parse(idString);
            }
            else if (id is ObjectId)
            {
                objectId = (ObjectId)id;
            }
            else
            {
                throw new ArgumentException("El tipo de ID proporcionado no es válido.");
            }

            // Crear una definición de actualización para los campos que quieres cambiar
            var update = Builders<User>.Update
                .Set(user => user.Name, resident.Name)
                .Set(user => user.Lastname, resident.Lastname)
                .Set(user => user.Email, resident.Email)
                .Set(user => user.Password, resident.Password)
                .Set(user => user.Identificacion, resident.Identificacion)
                .Set(user => user.Roles, resident.Roles)
                .Set(user => user.LastModifiedAt, DateTime.UtcNow)
                .Set(user => user.LastModifiedBy, objectId);  // Asegúrate de que esto sea el ID del usuario logueado

            // Realizar la actualización
            var result = await _users.UpdateOneAsync(user => user.Id == objectId, update);

            // Si no se actualizó ningún documento, lanzar una excepción
            if (result.ModifiedCount == 0)
            {
                throw new Exception("No se encontró ningún residente con este ID.");
            }

            // Devolver el residente actualizado
            return await _users.Find(user => user.Id == objectId).FirstOrDefaultAsync();
        }

    }
}
