using Microsoft.EntityFrameworkCore;
using Trackit.DAL.Entities;

namespace Trackit.Common.Tests.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity EmptyProject = new()
    {
        Id = default,
        Name = default!
    };

    public static readonly ProjectEntity SampleProject = new()
    {
        Id = Guid.Parse(input: "923788cf-a8c8-41c1-a322-222c0bb0cdc3"),
        Name = "ICS"
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly ProjectEntity SampleProjectUpdate = SampleProject with { Id = Guid.Parse("148c4624-ac82-4210-b5b2-1ea83c354808") };
    public static readonly ProjectEntity SampleProjectDelete = SampleProject with { Id = Guid.Parse("0b92bc09-4747-464d-80c1-b77d4e35f3ae") };

    public static ProjectEntity ProjectEntity1 = new()
    {
        Id = Guid.Parse(input: "446ecedf-7ca9-46d8-adef-1df7f3dc5752"),
        Name = "Running Brno"
    };

    public static ProjectEntity ProjectEntity2 = new()
    {
        Id = Guid.Parse(input: "2f2da71c-fc16-4c1e-acd1-fb12e79b2203"),
        Name = "Moist Esports"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectEntity1,
            ProjectEntity2,
            SampleProject,
            SampleProjectUpdate,
            SampleProjectDelete);
    }
}
