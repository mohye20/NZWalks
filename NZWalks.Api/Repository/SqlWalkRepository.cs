using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repository;

public class SqlWalkRepository : IWalkRepository
{
    private readonly NZWalksDbContext _dbContext;

    public SqlWalkRepository(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        return await _dbContext.Walks.Include(x => x.Diffculty).Include(x => x.Region).ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Walks.Include(x => x.Region).Include(x => x.Diffculty)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var walkDomainModel = await _dbContext.Walks.Include(x=>x.Diffculty).Include(x=>x.Region).FirstOrDefaultAsync(x => x.Id == id);

        if (walkDomainModel is null)
        {
            return null;
        }

        walkDomainModel.Name = walk.Name;
        walkDomainModel.LengthInKM = walk.LengthInKM;
        walkDomainModel.ImageUrl = walk.ImageUrl;
        walkDomainModel.Description = walk.Description;
        walkDomainModel.RegionId = walk.RegionId;
        walkDomainModel.DiffcultyId = walk.DiffcultyId;

        await _dbContext.SaveChangesAsync();
        
        return walkDomainModel;
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk is null)
        {
            return null;
        }
        
        _dbContext.Walks.Remove(existingWalk);
        await _dbContext.SaveChangesAsync();
        return existingWalk;
        ;
    }
}