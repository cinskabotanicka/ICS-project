using Microsoft.EntityFrameworkCore;
using Trackit.DAL.Entities;

namespace Trackit.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity Matej = new()
    {
        Id = Guid.Parse(input: "06a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
        FirstName = "Matej",
        LastName = "Rusinsky",
        PhotoUrl = @"https://images.generated.photos/qFDR4b6-8BP4-AmML85xZvA1ohejVA1qPwdWuE3plP0/rs:fit:256:256/czM6Ly9pY29uczgu/Z3Bob3Rvcy1wcm9k/LnBob3Rvcy92M18w/MDg0NzM1LmpwZw.jpg"
    };

    public static readonly UserEntity Karel = new()
    {
        Id = Guid.Parse(input: "df935095-8709-4040-a2bb-b6f97cb416dc"),
        FirstName = "Karel",
        LastName = "Kovacs",
        PhotoUrl = @"https://images.generated.photos/wRnZ-7Te2IruHKH2QJ5Z5GOmCezDa3zgZtkuLNwunNs/rs:fit:256:256/czM6Ly9pY29uczgu/Z3Bob3Rvcy1wcm9k/LnBob3Rvcy92M18w/OTE5MTMwLmpwZw.jpg"
    };


    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<UserEntity>().HasData(
            Matej,
            Karel
        );
}