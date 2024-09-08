using Trackit.Common.Enums;

namespace Trackit.DAL.Entities
{
    public record ActivityEntity : IEntity
    {
        public required Guid Id { get; set; }

        public required Guid UserId { get; set; }

        public required Guid ProjectId { get; set; }

        public UserEntity? User { get; init; }

        public ProjectEntity? Project { get; init; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public ActivityType Type { get; set; }

        public required DateTime Start { get; set; }

        public required DateTime End { get; set; }
    }
}
