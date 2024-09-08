using Trackit.BL.Models;
using Trackit.DAL.Entities;

namespace Trackit.BL.Facades.Interfaces;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    IEnumerable<ActivityListModel> GetByUserId(Guid id);
    IEnumerable<ActivityListModel> GetByProjectId(Guid id);
    IEnumerable<ActivityListModel> GetByUserIdDateRestrict(Guid id, DateTime value);
}
