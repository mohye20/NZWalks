using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repository;

public class SqlRegionRepository : IRegionRepository
{
    private readonly NZWalksDbContext _nzDbContext;

    public SqlRegionRepository(NZWalksDbContext nzDbContext)
    {
        _nzDbContext = nzDbContext;
    }
    
    public async Task<List<Region>> GetAllAsync()
    {
        return await _nzDbContext.Regions.ToListAsync();
    }
}