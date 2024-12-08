namespace NZWalks.Api.Models.DTO;

public class CreateWalksRequestDto
{

    public string Name { get; set; }

    public string Description { get; set; }

    public string LengthInKM { get; set; }

    public string? ImageUrl { get; set; }
    
    public Guid DiffcultyId { get; set; }

    public Guid RegionId { get; set; }
}