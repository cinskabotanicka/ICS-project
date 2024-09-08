using Microsoft.EntityFrameworkCore;
using Trackit.DAL.Entities;

namespace Trackit.DAL.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity Running = new()
    {
        Id = Guid.Parse(input: "446ecedf-7ca9-46d8-adef-1df7f3dc5752"),
        Name = "Running Brno"
    };

    public static readonly ProjectEntity Moist = new()
    {
        Id = Guid.Parse(input: "2f2da71c-fc16-4c1e-acd1-fb12e79b2203"),
        Name = "Moist Esports"
    };


    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ProjectEntity>().HasData(
            Running,
            Moist
        );
}