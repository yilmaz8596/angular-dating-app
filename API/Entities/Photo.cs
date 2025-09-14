

namespace API.Entities;

public class Photo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Url { get; set; } = null!;

    public string? PublicId { get; set; } = null!;

    public Member Member { get; set; } = null!;
    public string MemberId { get; set; } = null!;
}
