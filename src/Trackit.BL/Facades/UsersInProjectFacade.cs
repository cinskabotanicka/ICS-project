using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Mappers.Interfaces;
using Trackit.BL.Models;
using Trackit.DAL.Entities;
using Trackit.DAL.Mappers;
using Trackit.DAL.Repositories;
using Trackit.DAL.UnitOfWork;

namespace Trackit.BL.Facades;

public class UsersInProjectFacade :
    FacadeBase<UsersInProjectEntity, UsersInProjectListModel, UsersInProjectDetailModel,
        UsersInProjectEntityMapper>, IUsersInProjectFacade
{
    private readonly IUsersInProjectModelMapper _usersInProjectModelMapper;

    public UsersInProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUsersInProjectModelMapper usersInProjectModelMapper)
        : base(unitOfWorkFactory, usersInProjectModelMapper) =>
        _usersInProjectModelMapper = usersInProjectModelMapper;

    public async Task SaveAsync(UsersInProjectListModel model, Guid projectId)
    {
        UsersInProjectEntity entity = _usersInProjectModelMapper.MapToEntity(model, projectId);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<UsersInProjectEntity> repository =
            uow.GetRepository<UsersInProjectEntity, UsersInProjectEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            await repository.UpdateAsync(entity);
            await uow.CommitAsync();
        }
    }

    public async Task<UsersInProjectDetailModel> SaveAsync(UsersInProjectDetailModel model, Guid projectId)
    {
        UsersInProjectEntity entity = _usersInProjectModelMapper.MapToEntity(model, projectId);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<UsersInProjectEntity> repository =
            uow.GetRepository<UsersInProjectEntity, UsersInProjectEntityMapper>();

        await repository.InsertAsync(entity);
        await uow.CommitAsync();

        return ModelMapper.MapToDetailModel(entity);
    }

    public void DeleteByUserAndProject(Guid projectId, Guid userId)
    {
        IRepository<UsersInProjectEntity> uipRepo = UnitOfWorkFactory.Create().GetRepository<UsersInProjectEntity, UsersInProjectEntityMapper>();
        var uipEntities = uipRepo.Get();
        var filteredUIP = uipEntities.Where(u => u.UserId == userId && u.ProjectId == projectId);

        var UIP = filteredUIP.ToList();
        if (UIP.Count == 0) return;
        _ = DeleteAsync(UIP[0].Id);
    }
}
