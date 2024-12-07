namespace NZWalks.Api.Models.Domain;

public class Walk
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string LengthInKM { get; set; }

    public string? ImageUrl { get; set; }

    public Guid DiffcultyId { get; set; }

    public Guid RegionId { get; set; }


    // Navigational Property

    public Diffculty Diffculty { get; set; }

    public Region Region { get; set; }
}