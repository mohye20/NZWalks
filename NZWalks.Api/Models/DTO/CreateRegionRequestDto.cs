using System.ComponentModel.DataAnnotations;

namespace NZWalks.Api.Models.DTO;

public class CreateRegionRequestDto
{
    [Required]
    [MaxLength(3, ErrorMessage = "Code Max length 3")]
    [MinLength(3, ErrorMessage = "Code Min length 3")]
    public string Code { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Code Max length 100")]
    public string Name { get; set; }

    public string? RegionImageUrl { get; set; }
}