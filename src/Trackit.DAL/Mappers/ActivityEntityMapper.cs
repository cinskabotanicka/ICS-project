using Trackit.DAL.Entities;

namespace Trackit.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.ProjectId = newEntity.ProjectId;
        existingEntity.Name = newEntity.Name;           
        existingEntity.Description = newEntity.Description;
        existingEntity.Type = newEntity.Type;
        existingEntity.Start = newEntity.Start;
        existingEntity.End = newEntity.End;
    }
}