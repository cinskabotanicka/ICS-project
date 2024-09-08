namespace Trackit.DAL.Entities;

public record ProjectEntity : IEntity
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public ICollection<UsersInProjectEntity> Users { get; set; } = new List<UsersInProjectEntity>();

    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
}
