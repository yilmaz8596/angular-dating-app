

namespace API.DTOs;


public class SeedUserDTO
{
    public required string Id { get; set; }
    public string Email { get; set; }

    public DateOnly DateofBirth { get; set; }
    public string? ImageUrl { get; set; }

    public required string DisplayName { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public required string Gender { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public required string City { get; set; } = null!;

    public required string Country { get; set; } = null!;


}