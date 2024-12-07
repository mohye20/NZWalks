using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions options) : base(options)
    {
    }


    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
    public DbSet<Diffculty> Diffculties { get; set; }
}