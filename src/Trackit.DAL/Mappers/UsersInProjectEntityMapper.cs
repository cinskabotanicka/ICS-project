using Trackit.DAL.Entities;

namespace Trackit.DAL.Mappers;
public class UsersInProjectEntityMapper : IEntityMapper<UsersInProjectEntity>
{
    public void MapToExistingEntity(UsersInProjectEntity existingEntity, UsersInProjectEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.UserId = newEntity.UserId;
        existingEntity.ProjectId = newEntity.ProjectId;
    }
}