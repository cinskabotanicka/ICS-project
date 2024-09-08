using Trackit.BL.Models;
using Trackit.DAL.Entities;

namespace Trackit.BL.Mappers.Interfaces;

public interface IProjectModelMapper : IModelMapper<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
}
