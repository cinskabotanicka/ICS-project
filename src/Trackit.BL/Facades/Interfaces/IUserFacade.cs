using Trackit.BL.Models;
using Trackit.DAL.Entities;

namespace Trackit.BL.Facades.Interfaces;

public interface IUserFacade : IFacade<UserEntity, UserListModel, UserDetailModel>
{
    public IEnumerable<UserListModel> GetByIdArray(List<UsersInProjectListModel> array);
}
