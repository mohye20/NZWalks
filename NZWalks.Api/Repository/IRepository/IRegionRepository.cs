using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repository;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
}