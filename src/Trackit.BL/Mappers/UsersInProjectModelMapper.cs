using Trackit.BL.Mappers.Interfaces;
using Trackit.BL.Models;
using Trackit.DAL.Entities;

namespace Trackit.BL.Mappers;
public class UsersInProjectModelMapper : ModelMapperBase<UsersInProjectEntity, UsersInProjectListModel, UsersInProjectDetailModel>,
    IUsersInProjectModelMapper
{
    public override UsersInProjectListModel MapToListModel(UsersInProjectEntity? entity)
        => entity is null
            ? UsersInProjectListModel.Empty
            : new UsersInProjectListModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId
            };

    public override UsersInProjectDetailModel MapToDetailModel(UsersInProjectEntity? entity)
        => entity is null
            ? UsersInProjectDetailModel.Empty
            : new UsersInProjectDetailModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId,
                User = entity.User,
                Project = entity.Project
            };

    public void MapToExistingDetailModel(UsersInProjectDetailModel existingDetailModel,
        UserListModel user)
    {
        existingDetailModel.User!.FirstName = user.FirstName;
        existingDetailModel.User.LastName = user.LastName;
        existingDetailModel.User.PhotoUrl = user.PhotoUrl;
    }

    public UsersInProjectListModel MapToListModel(UsersInProjectDetailModel detailModel)
        => new()
        {
            UserId = detailModel.UserId,
            ProjectId = detailModel.ProjectId,
        };

    public override UsersInProjectEntity MapToEntity(UsersInProjectDetailModel model)
    => throw new NotImplementedException("This method is unsupported. Use the other overload.");

    public UsersInProjectEntity MapToEntity(UsersInProjectDetailModel model, Guid projectId)
        => new()
        {
            Id = model.Id,
            UserId = model.UserId,
            ProjectId = projectId,
            User = model.User,
            Project = model.Project

        };

    public UsersInProjectEntity MapToEntity(UsersInProjectListModel model, Guid projectId)
    => new()
    {
        Id = model.Id,
        UserId = model.UserId,
        ProjectId = model.ProjectId,
    };
}
