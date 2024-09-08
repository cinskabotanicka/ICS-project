namespace Trackit.DAL.Entities
{
    public record UsersInProjectEntity : IEntity
    {
        public Guid Id { get; set; }

        public required Guid UserId { get; set; }

        public required Guid ProjectId { get; set; }

        public UserEntity? User { get; init; }

        public ProjectEntity? Project { get; init; }
    }
}
