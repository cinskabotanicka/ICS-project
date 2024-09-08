using Microsoft.EntityFrameworkCore;
using Trackit.DAL.Entities;

namespace Trackit.DAL.Seeds;

public static class UsersInProjectSeeds
{
    public static readonly UsersInProjectEntity Runners = new()
    {
        Id = Guid.Parse(input: "0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        UserId = UserSeeds.Matej.Id,
        ProjectId = ProjectSeeds.Running.Id
    };

    public static readonly UsersInProjectEntity Speedrunners = new()
    {
        Id = Guid.Parse(input: "87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
        UserId = UserSeeds.Karel.Id,
        ProjectId = ProjectSeeds.Moist.Id
    };

    public static readonly UsersInProjectEntity ValoProPlayers = new()
    {
        Id = Guid.Parse(input: "87833e66-05ff-4d6b-900b-fe5ace88dbd8"),
        UserId = UserSeeds.Matej.Id,
        ProjectId = ProjectSeeds.Moist.Id
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<UsersInProjectEntity>().HasData(
            Runners with { User = null, Project = null },
            Speedrunners with { User = null, Project = null },
            ValoProPlayers with { User = null, Project = null }
        );
}