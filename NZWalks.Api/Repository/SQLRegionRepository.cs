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

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        var existingRegion = await _nzDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (existingRegion is null)
        {
            return null;
        }

        return existingRegion;
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _nzDbContext.Regions.AddAsync(region);
        await _nzDbContext.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existingRegion = await _nzDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (existingRegion is null)
        {
            return null;
        }

        existingRegion.Name = region.Name;
        existingRegion.Code = region.Code;
        existingRegion.RegionImageUrl = region.RegionImageUrl;

        await _nzDbContext.SaveChangesAsync();

        return existingRegion;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var existingRegion = await _nzDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (existingRegion is null)
        {
            return null;
        }

        _nzDbContext.Regions.Remove(existingRegion);
        await _nzDbContext.SaveChangesAsync();
        return existingRegion;
    }
}