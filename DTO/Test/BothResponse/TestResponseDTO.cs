using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class TestResponseDTO
{
    public int Id { get; set; }
    public bool Status { get; set; } = true;
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public string Instruction { get; set; } = "";

    public DateTime CreatedAt { get; set; }

    public int TestType { get; set; }

    public int? TopicId { get; set; }

    public int? SubjectId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? ResultDate { get; set; }

    public int? TestDuration { get; set; }
}
