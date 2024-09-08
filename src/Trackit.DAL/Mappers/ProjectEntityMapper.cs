using Trackit.DAL.Entities;

namespace Trackit.DAL.Mappers;
public class ProjectEntityMapper : IEntityMapper<ProjectEntity>
{
    public void MapToExistingEntity(ProjectEntity existingEntity, ProjectEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.Name = newEntity.Name;
    }
}