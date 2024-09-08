using Microsoft.EntityFrameworkCore;
using Trackit.DAL.Entities;
using Trackit.Common.Enums;

namespace Trackit.Common.Tests.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity EmptyActivity = new()
    { 
        Id = default,
        Name = default!,
        Description = default!,
        Type = default!,
        UserId = default!,
        ProjectId = default!,
        Start = default!,
        End = default!,
    };

    public static readonly ActivityEntity MorningRun = new()
    { 
        Id = Guid.Parse(input: "06a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
        Name = "Morning run",
        Description = "Light jog around Brno",
        Type = ActivityType.Sport,
        UserId = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        Start = DateTime.Parse("Tue, 1 Jan 2008 00:00:00Z"),
        End = DateTime.Parse("Tue, 1 Jan 2008 01:00:00Z"),
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly ActivityEntity MorningRunUpdate = MorningRun with { Id = Guid.Parse("143332B9-080E-4953-AEA5-BEF64679B052"), User = null, Project = null, ProjectId = ProjectSeeds.SampleProjectUpdate.Id };
    public static readonly ActivityEntity MorningRunDelete = MorningRun with { Id = Guid.Parse("274D0CC9-A948-4818-AADB-A8B4C0506619"), User = null, Project = null, ProjectId = ProjectSeeds.SampleProjectUpdate.Id };

    public static ActivityEntity ActivityEntity1 = new()
    { 
        Id = Guid.Parse(input: "df935095-8709-4040-a2bb-b6f97cb416dc"),
        Name = "ICS projekt",
        Description = "Last minute speedrun",
        Type = ActivityType.School,
        UserId = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity2.Id,
        Start = DateTime.Parse("Sun, 9 Apr 2023 11:26:11Z"),
        End = DateTime.Parse("Sun, 9 Apr 2023 23:59:59Z"),
    };

    public static ActivityEntity ActivityEntity2 = new()
    { 
        Id = Guid.Parse(input: "23b3902d-7d4f-4213-9cf0-112348f56238"),
        Name = "Guitar practice",
        Description = "Start with some warm-up exercises, then practice Clair de Lune",
        Type = ActivityType.Hobby,
        UserId = UserSeeds.UserEntity2.Id,
        ProjectId = ProjectSeeds.ProjectEntity2.Id,
        Start = DateTime.Parse("Sun, 15 Jan 2023 11:26:11Z"),
        End = DateTime.Parse("Sun, 15 Jan 2023 12:15:13Z"),
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEntity1,
            ActivityEntity2,
            MorningRun,
            MorningRunUpdate,
            MorningRunDelete);
    }
}
