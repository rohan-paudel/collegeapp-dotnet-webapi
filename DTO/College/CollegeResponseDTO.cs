namespace CollegeAppDotnetWebApi;

public class CollegeResponseDTO
{
    public string? CollegeId { get; set; }

    public string? Name { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public string? Address { get; set; }

    public string? LocationUrl { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? IconUrl { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? Description { get; set; }

    public int StudentCount { get; set; } = 0;
}

public class CollegeOnlyNameResponseDTO
{
    public string? Id { get; set; }

    public string? Name { get; set; }
}
