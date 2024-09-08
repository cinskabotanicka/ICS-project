using Trackit.Common.Enums;

namespace Trackit.BL.Models;

public record ActivityListModel : ModelBase
{
    public required Guid UserId { get; set; }

    public required Guid ProjectId { get; set; }

    public required string Name { get; set; }

    public ActivityType Type { get; set; }

    public required DateTime Start { get; set; }

    public required DateTime End { get; set; }


    public static ActivityListModel Empty => new()
    {
        Id = Guid.Empty,
        UserId = Guid.Empty,
        ProjectId = Guid.Empty,
        Name = string.Empty,
        Type = ActivityType.None,
        Start = DateTime.MinValue,
        End = DateTime.MaxValue,
    };
}
