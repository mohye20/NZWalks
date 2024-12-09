using System.ComponentModel.DataAnnotations;

namespace NZWalks.Api.Models.DTO;

public class CreateWalksRequestDto
{
    [Required]
    [MaxLength(100, ErrorMessage = "Name Max Length 100")]
    public string Name { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Description Max Length 100")]
    public string Description { get; set; }

    [Required]
    [Range(0, 50)] 
    public double LengthInKM { get; set; }

    public string? ImageUrl { get; set; }

    [Required] 
    public Guid DiffcultyId { get; set; }

    [Required] 
    public Guid RegionId { get; set; }
}