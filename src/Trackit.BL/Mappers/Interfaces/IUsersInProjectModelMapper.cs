using Trackit.BL.Models;
using Trackit.DAL.Entities;

namespace Trackit.BL.Mappers.Interfaces;

public interface IUsersInProjectModelMapper : IModelMapper<UsersInProjectEntity, UsersInProjectListModel, UsersInProjectDetailModel>
{
    UsersInProjectListModel MapToListModel(UsersInProjectDetailModel detailModel);
    UsersInProjectEntity MapToEntity(UsersInProjectDetailModel model, Guid projectId);
    void MapToExistingDetailModel(UsersInProjectDetailModel existingDetailModel, UserListModel user);
    UsersInProjectEntity MapToEntity(UsersInProjectListModel model, Guid projectId);
}
