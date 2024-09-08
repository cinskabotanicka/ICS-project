using Trackit.BL.Models;

namespace Trackit.BL.Facades.Interfaces;

public interface IUsersInProjectFacade
{
    Task<UsersInProjectDetailModel> SaveAsync(UsersInProjectDetailModel model, Guid projectId);
    Task SaveAsync(UsersInProjectListModel model, Guid projectId);
    Task DeleteAsync(Guid id);
    public void DeleteByUserAndProject(Guid projectId, Guid userId);
}
