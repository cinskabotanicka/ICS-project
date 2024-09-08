using Trackit.BL.Mappers.Interfaces;
using Trackit.BL.Models;
using Trackit.DAL.Entities;

namespace Trackit.BL.Mappers;

public class ActivityModelMapper : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>,
    IActivityModelMapper
{
    public override ActivityListModel MapToListModel(ActivityEntity? entity)
        => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                Type = entity.Type,
                Start = entity.Start,
                End = entity.End
            };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
        => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type,
                Start = entity.Start,
                End = entity.End,
                Project = entity.Project,
                User = entity.User
            };

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            UserId = model.UserId,
            ProjectId = model.ProjectId,
            Name = model.Name,
            Description = model.Description,
            Type = model.Type,
            Start = model.Start,
            End = model.End,
            Project = model.Project,
            User = model.User
        };
}
