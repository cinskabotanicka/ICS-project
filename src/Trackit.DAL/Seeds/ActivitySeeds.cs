using Microsoft.EntityFrameworkCore;
using Trackit.Common.Enums;
using Trackit.DAL.Entities;

namespace Trackit.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity BrnoJog = new()
    {
        Id = Guid.Parse(input: "06a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
        Name = "Morning run",
        Description = "Light jog around Brno",
        Type = ActivityType.Sport,
        UserId = UserSeeds.Matej.Id,
        ProjectId = ProjectSeeds.Running.Id,
        Project = ProjectSeeds.Running,
        Start = DateTime.Parse("Tue, 1 Jan 2008 00:00:00Z"),
        End = DateTime.Parse("Tue, 1 Jan 2008 01:00:00Z"),
    };

    public static readonly ActivityEntity BodybuildingSession = new()
    {
        Id = Guid.Parse(input: "62ce6e7d-b414-4358-9791-c78cf0c83ec1"),
        Name = "Bodybuilding session",
        Description = "Heavy squats with Dominik",
        Type = ActivityType.Sport,
        UserId = UserSeeds.Matej.Id,
        ProjectId = ProjectSeeds.Running.Id,
        Project = ProjectSeeds.Running,
        Start = DateTime.Parse("Fri, 4 Jan 2008 01:00:00Z"),
        End = DateTime.Parse("Fri, 4 Jan 2008 05:00:00Z"),
    };

    public static readonly ActivityEntity Speedrun = new()
    {
        Id = Guid.Parse(input: "df935095-8709-4040-a2bb-b6f97cb416dc"),
        Name = "ICS projekt",
        Description = "TODO",
        Type = ActivityType.School,
        UserId = UserSeeds.Karel.Id,
        ProjectId = ProjectSeeds.Moist.Id,
        Project = ProjectSeeds.Moist,
        Start = DateTime.Parse("Sun, 9 Apr 2023 11:26:11Z"),
        End = DateTime.Parse("Sun, 9 Apr 2023 23:59:59Z"),
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ActivityEntity>().HasData(
            BrnoJog with { User = null, Project = null },
            BodybuildingSession with { User = null, Project = null },
            Speedrun with { User = null, Project = null }
        );
}