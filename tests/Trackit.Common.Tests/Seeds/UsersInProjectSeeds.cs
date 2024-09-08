using Trackit.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Trackit.Common.Tests.Seeds;

public static class UsersInProjectSeeds
{
    public static readonly UsersInProjectEntity EmptyUsersInProjectEntity = new()
    {
        Id = default,
        UserId = default,
        ProjectId = default,
    };

    public static readonly UsersInProjectEntity UsersInProjectEntity1 = new()
    {
        Id = Guid.Parse(input: "0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        UserId = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
    };

    public static readonly UsersInProjectEntity UsersInProjectEntity2 = new()
    {
        Id = Guid.Parse(input: "f147191d-9912-4339-bd6d-1193bece6df3"),
        UserId = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity2.Id,
    };

    public static readonly UsersInProjectEntity UsersInProjectEntity3 = new()
    {
        Id = Guid.Parse(input: "87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
        UserId = UserSeeds.UserEntity2.Id,
        ProjectId = ProjectSeeds.ProjectEntity2.Id,
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UsersInProjectEntity UsersInProjectEntityUpdate = UsersInProjectEntity1 with { Id = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674"), User = null, Project = null, ProjectId = ProjectSeeds.SampleProjectUpdate.Id };
    public static readonly UsersInProjectEntity UsersInProjectEntityDelete = UsersInProjectEntity1 with { Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279"), User = null, Project = null, ProjectId = ProjectSeeds.SampleProjectUpdate.Id };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsersInProjectEntity>().HasData(
            UsersInProjectEntity1 with { User = null, Project = null },
            UsersInProjectEntity2 with { User = null, Project = null },
            UsersInProjectEntity3 with { User = null, Project = null },
            UsersInProjectEntityUpdate,
            UsersInProjectEntityDelete
        );
    }
}