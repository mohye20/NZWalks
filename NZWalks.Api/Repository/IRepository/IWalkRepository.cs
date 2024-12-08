using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repository;

public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);
    
    Task<List<Walk>> GetAllAsync();
    
    Task<Walk?> GetByIdAsync(Guid id);
}