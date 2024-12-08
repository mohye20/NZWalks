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
}