namespace NZWalks.Api.Models.DTO;

public class CreateRegionRequestDto
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string? RegionImageUrl { get; set; }
}