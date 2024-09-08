using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Mappers.Interfaces;
using Trackit.BL.Models;
using Trackit.DAL.Entities;
using Trackit.DAL.Mappers;
using Trackit.DAL.Repositories;
using Trackit.DAL.UnitOfWork;

namespace Trackit.BL.Facades;

public class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>, IProjectFacade
{
    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    protected override string IncludesNavigationPathDetail =>
        $"{nameof(ProjectEntity.Users)}.{nameof(UsersInProjectEntity.User)}";

    protected string IncludeNavigationPathDetail2 =>
        $"{nameof(ProjectEntity.Activities)}";

    public IEnumerable<ProjectListModel> GetByUserId(Guid id)
    {
        IRepository<UsersInProjectEntity> usersInProjectRepo = UnitOfWorkFactory.Create().GetRepository<UsersInProjectEntity, UsersInProjectEntityMapper>();
        var usersInProjectEntities = usersInProjectRepo.Get();
        var filteredUsersInProject = usersInProjectEntities.Where(UIP => UIP.UserId == id);

        List<UsersInProjectEntity> usersInProject = filteredUsersInProject.ToList();
        var tmp = new List<Guid>();
        for (int i = 0; i < usersInProject.Count; i++)
            tmp.Add(usersInProject[i].ProjectId);

        IRepository<ProjectEntity> projectRepo = UnitOfWorkFactory.Create().GetRepository<ProjectEntity, ProjectEntityMapper>();
        var projectEntities = projectRepo.Get();
        var filteredProjects = projectEntities.Where(Project => tmp.Contains(Project.Id));

        List<ProjectEntity> Projects = filteredProjects.ToList();

        return ModelMapper.MapToListModel(Projects);
    }

    public IEnumerable<ProjectListModel> GetById(Guid id)
    {
        IRepository<ProjectEntity> projectRepo = UnitOfWorkFactory.Create().GetRepository<ProjectEntity, ProjectEntityMapper>();
        var projectEntities = projectRepo.Get();
        var filteredProjects = projectEntities.Where(Project => Project.Id == id);

        List<ProjectEntity> Projects = filteredProjects.ToList();

        return ModelMapper.MapToListModel(Projects);
    }
}