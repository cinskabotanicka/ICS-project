using Trackit.DAL.Entities;

namespace Trackit.DAL.Mappers;
public class UserEntityMapper : IEntityMapper<UserEntity>
{
    public void MapToExistingEntity(UserEntity existingEntity, UserEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.FirstName = newEntity.FirstName;
        existingEntity.LastName = newEntity.LastName;
        existingEntity.PhotoUrl = newEntity.PhotoUrl;
    }
}