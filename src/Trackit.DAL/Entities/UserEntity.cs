namespace Trackit.DAL.Entities;

public record UserEntity : IEntity
{
    public required Guid Id { get; set; }
    
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? PhotoUrl { get; set; }

    public ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();
}
