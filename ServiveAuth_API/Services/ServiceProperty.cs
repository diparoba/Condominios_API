﻿using MongoDB.Bson;
using MongoDB.Driver;
using ServiceAuth_API.DTO;
using ServiceAuth_API.Models;

namespace ServiceAuth_API.Services
{
    public class ServiceProperty : IServiceProperty
    {
        private readonly MongoDBRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Property> _properties;

        public ServiceProperty(IConfiguration configuration, MongoDBRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
            _users = _repository.database.GetCollection<User>("Users");
            _properties = _repository.database.GetCollection<Property>("Properties");
        }
        public async Task<Property> CreateProperty(Property property, ObjectId ownerId)
        {
            // Fetch the proprietor from the database
            var proprietor = await _users.Find(user => user.Id == ownerId && user.Roles.Contains("Propietario") && user.Status == "A").FirstOrDefaultAsync();
            if (proprietor == null)
            {
                throw new Exception("No se encontró ningún usuario con este ID que sea propietario y esté activo.");
            }

            // Set the ownerId field of the property
            property.OwnerId = ownerId.ToString();

            // Insert the new property into the database
            await _properties.InsertOneAsync(property);

            return property;
        }

        public async Task<IEnumerable<PropertyDTO>> GetAllProperties()
        {
            var properties = await _properties.Find(property => true).ToListAsync();
            var propertyDTOs = new List<PropertyDTO>();

            foreach (var property in properties)
            {
                var ownerId = new ObjectId(property.OwnerId);
                var proprietor = await _users.Find(user => user.Id.Equals(ownerId)).FirstOrDefaultAsync();

                if (proprietor == null)
                {
                    throw new Exception("No se encontró ningún propietario con este ID.");
                }

                var propertyDTO = new PropertyDTO
                {
                    Name = property.Name,
                    Address = property.Address,
                    UnitNumber = property.UnitNumber,
                    Size = property.Size,
                    Amenities = property.Amenities,
                    OwnerId = property.OwnerId,
                    OwnerName = proprietor.Name + " " + proprietor.Lastname,
                    Status = property.Status
                };

                propertyDTOs.Add(propertyDTO);
            }

            return propertyDTOs;
        }

        public async Task<Property> GetPropertyById(string id)
        {
            var propertyId = new ObjectId(id);
            var property = await _properties.Find(property => property.Id == propertyId).FirstOrDefaultAsync();

            if (property == null)
            {
                throw new Exception("No se encontró ninguna propiedad con este ID.");
            }

            return property;
        }

        public async Task RemoveProperty(Property propertyIn)
        {
            await _properties.DeleteOneAsync(property => property.Id == propertyIn.Id);
        }

        public async Task RemovePropertyById(string id)
        {
            var propertyId = new ObjectId(id);
            await _properties.DeleteOneAsync(property => property.Id == propertyId);
        }

        public async Task UpdateProperty(string id, Property propertyIn)
        {
            var propertyId = new ObjectId(id);
            await _properties.ReplaceOneAsync(property => property.Id == propertyId, propertyIn);
        }
    }
}
