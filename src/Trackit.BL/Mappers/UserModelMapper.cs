using Trackit.BL.Mappers.Interfaces;
using Trackit.BL.Models;
using Trackit.DAL.Entities;

namespace Trackit.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>,
    IUserModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;

    public UserModelMapper(IActivityModelMapper activityModelMapper) =>
        _activityModelMapper = activityModelMapper;

    public override UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhotoUrl = entity.PhotoUrl
            };

    public override UserDetailModel MapToDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhotoUrl = entity.PhotoUrl,
                Activities = _activityModelMapper.MapToListModel(entity.Activities)
                    .ToObservableCollection()
            };

    public override UserEntity MapToEntity(UserDetailModel model)
        => new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhotoUrl = model.PhotoUrl
        };
}
