using Trackit.DAL.Entities;

namespace Trackit.DAL.Mappers; 

public interface IEntityMapper<in TEntity>
    where TEntity : IEntity
{
    void MapToExistingEntity(TEntity existingEntity, TEntity newEntity);
}
