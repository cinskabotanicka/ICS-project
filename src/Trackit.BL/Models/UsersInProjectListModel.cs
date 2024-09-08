namespace Trackit.BL.Models;

public record UsersInProjectListModel : ModelBase
{
    public required Guid UserId { get; set; }

    public required Guid ProjectId { get; set; }


    public static UsersInProjectListModel Empty => new()
    {
        UserId = Guid.Empty,
        ProjectId = Guid.Empty
    };
}