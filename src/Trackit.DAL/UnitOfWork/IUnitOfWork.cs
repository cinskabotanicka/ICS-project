using Trackit.DAL.Entities;
using Trackit.DAL.Mappers;
using Trackit.DAL.Repositories;

namespace Trackit.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity, TEntityMapper>()
        where TEntity : class, IEntity
        where TEntityMapper : IEntityMapper<TEntity>, new();

    Task CommitAsync();
}