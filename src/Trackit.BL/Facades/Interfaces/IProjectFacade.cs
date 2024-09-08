using Trackit.BL.Models;
using Trackit.DAL.Entities;

namespace Trackit.BL.Facades.Interfaces;

public interface IProjectFacade : IFacade<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
    public IEnumerable<ProjectListModel> GetByUserId(Guid id);
    public IEnumerable<ProjectListModel> GetById(Guid id);
}
