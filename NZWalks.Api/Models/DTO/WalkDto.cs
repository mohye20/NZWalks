namespace NZWalks.Api.Models.DTO;

public class WalkDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string LengthInKM { get; set; }

    public string? ImageUrl { get; set; }



    public RegionDTO region { get; set; }

    public DiffcultyDto diffculty { get; set; }
}