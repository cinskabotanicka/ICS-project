using Trackit.DAL.Entities;

namespace Trackit.BL.Models;

public record UsersInProjectDetailModel : ModelBase
{
    public required Guid UserId { get; set; }

    public required Guid ProjectId { get; set; }

    public UserEntity? User { get; set; }

    public ProjectEntity? Project { get; set; }


    public static UsersInProjectDetailModel Empty => new()
    {
        UserId = Guid.Empty,
        ProjectId = Guid.Empty,
        User = null,
        Project = null
    };
}