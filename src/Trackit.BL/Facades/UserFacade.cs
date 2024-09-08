using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Mappers.Interfaces;
using Trackit.BL.Models;
using Trackit.DAL.Entities;
using Trackit.DAL.Mappers;
using Trackit.DAL.Repositories;
using Trackit.DAL.UnitOfWork;

namespace Trackit.BL.Facades;

public class UserFacade : FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>, IUserFacade
{
    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    public IEnumerable<UserListModel> GetByIdArray(List<UsersInProjectListModel> array)
    {
        IRepository<UserEntity> userRepo = UnitOfWorkFactory.Create().GetRepository<UserEntity, UserEntityMapper>();
        var userEntities = userRepo.Get().ToList();
        var tmp = new List<Guid>();
        for (int i = 0; i < array.Count; i++)
            tmp.Add(array[i].UserId);
        var filteredUsers = userEntities.Where(user => tmp.Contains(user.Id));

        List<UserEntity> users = filteredUsers.ToList();

        return ModelMapper.MapToListModel(users);
    }

    protected override string IncludesNavigationPathDetail =>
        $"{nameof(UserEntity.Activities)}";
}
