using ServiceUser_API.Models;

namespace ServiceUser_API.Services
{
    public interface IServiceRepresentative
    {
        Task<List<Representative>> GetRepresentativesAsync();
        Task<Representative> GetRepresentativeAsync(string id);
        Task<Representative> CreateRepresentativeAsync(Representative representative);
        Task UpdateRepresentativeAsync(string id, Representative representative);
        Task DeleteRepresentativeAsync(string id);
    }
}
