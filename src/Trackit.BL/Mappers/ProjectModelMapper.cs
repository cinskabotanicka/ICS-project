using Trackit.BL.Mappers.Interfaces;
using Trackit.BL.Models;
using Trackit.DAL.Entities;

namespace Trackit.BL.Mappers;

public class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>,
    IProjectModelMapper
{
    private readonly IUsersInProjectModelMapper _usersInProjectModelMapper;
    private readonly IActivityModelMapper _activityModelMapper;

    public ProjectModelMapper(IUsersInProjectModelMapper usersInProjectModelMapper, IActivityModelMapper activityModelMapper) 
    {
        _usersInProjectModelMapper = usersInProjectModelMapper;
        _activityModelMapper = activityModelMapper;
    }


    public override ProjectListModel MapToListModel(ProjectEntity? entity)
        => entity is null
            ? ProjectListModel.Empty
            : new ProjectListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Users = _usersInProjectModelMapper.MapToListModel(entity.Users)
                    .ToObservableCollection()
            };

    public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
        => entity is null
            ? ProjectDetailModel.Empty
            : new ProjectDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Users = _usersInProjectModelMapper.MapToListModel(entity.Users)
                    .ToObservableCollection(),
                Activities = _activityModelMapper.MapToListModel(entity.Activities)
                    .ToObservableCollection()
            };

    public override ProjectEntity MapToEntity(ProjectDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name
        };
}
