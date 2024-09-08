using Trackit.Common.Enums;
using Trackit.DAL.Entities;

namespace Trackit.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required Guid UserId { get; set; }

    public required Guid ProjectId { get; set; }

    public UserEntity? User { get; set; }

    public ProjectEntity? Project { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public ActivityType Type { get; set; }

    public required DateTime Start { get; set; }

    public required DateTime End { get; set; }


    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.Empty,
        UserId = Guid.Empty,
        ProjectId = Guid.Empty,
        User = null,
        Project = null,
        Name = string.Empty,
        Description = string.Empty,
        Type = ActivityType.None,
        Start = DateTime.MinValue,
        End = DateTime.MaxValue
    };
}
