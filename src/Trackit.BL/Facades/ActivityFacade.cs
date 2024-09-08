using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Mappers.Interfaces;
using Trackit.BL.Models;
using Trackit.DAL.Entities;
using Trackit.DAL.Mappers;
using Trackit.DAL.Repositories;
using Trackit.DAL.UnitOfWork;

namespace Trackit.BL.Facades;

public class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>, IActivityFacade
{
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    public IEnumerable<ActivityListModel> GetByUserId(Guid id)
    {
        IRepository<ActivityEntity> activityRepo = UnitOfWorkFactory.Create().GetRepository<ActivityEntity, ActivityEntityMapper>();
        var activityEntities = activityRepo.Get();
        var filteredActivities = activityEntities.Where(activity => activity.UserId == id);

        List<ActivityEntity> activities = filteredActivities.ToList();

        return ModelMapper.MapToListModel(activities);
    }

    public IEnumerable<ActivityListModel> GetByProjectId(Guid id)
    {
        IRepository<ActivityEntity> activityRepo = UnitOfWorkFactory.Create().GetRepository<ActivityEntity, ActivityEntityMapper>();
        var activityEntities = activityRepo.Get();
        var filteredActivities = activityEntities.Where(activity => activity.ProjectId == id);

        List<ActivityEntity> activities = filteredActivities.ToList();

        return ModelMapper.MapToListModel(activities);
    }

    public IEnumerable<ActivityListModel> GetByUserIdDateRestrict(Guid id, DateTime value)
    {
        IRepository<ActivityEntity> activityRepo = UnitOfWorkFactory.Create().GetRepository<ActivityEntity, ActivityEntityMapper>();
        var activityEntities = activityRepo.Get();
        var filteredActivities = activityEntities.Where(activity => activity.UserId == id);
        var dateFilteredActivities = filteredActivities.Where(activity => activity.Start > value);

        List<ActivityEntity> activities = dateFilteredActivities.ToList();

        return ModelMapper.MapToListModel(activities);
    }
}
