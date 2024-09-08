using Microsoft.EntityFrameworkCore;
using Trackit.DAL.Entities;

namespace Trackit.Common.Tests.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity EmptyUser = new()
    { 
        Id = default,
        FirstName = default!,
        LastName = default!,
        PhotoUrl = default!
    };

    public static readonly UserEntity Matej = new()
    { 
        Id = Guid.Parse(input: "06a8a2cf-ea03-4095-a3e4-aa0291fe9c75"),
        FirstName = "Matej",
        LastName = "Rusinsky",
        PhotoUrl = "https://styles.redditmedia.com/t5_7u6e9g/styles/communityIcon_3k35hqrc97ga1.png"
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserEntity MatejUpdate = Matej with { Id = Guid.Parse("143332B9-080E-4953-AEA5-BEF64679B052") };
    public static readonly UserEntity MatejDelete = Matej with { Id = Guid.Parse("274D0CC9-A948-4818-AADB-A8B4C0506619") };

    public static UserEntity UserEntity1 = new()
    { 
        Id = Guid.Parse(input: "df935095-8709-4040-a2bb-b6f97cb416dc"),
        FirstName = "Karel",
        LastName = "Kovacs",
        PhotoUrl = "https://www.gravatar.com/avatar/1b8cc7489301a1c910cc96a28ca152ac?s=256&d=identicon&r=PG"
    };

    public static UserEntity UserEntity2 = new()
    { 
        Id = Guid.Parse(input: "23b3902d-7d4f-4213-9cf0-112348f56238"),
        FirstName = "Danka",
        LastName = "Hlavata",
        PhotoUrl = "https://a.thumbs.redditmedia.com/-eR7pbMu34Y4JXs8vV4phcBO-wjB7ksp__LWMgqDfb4.png"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity1,
            UserEntity2,
            Matej,
            MatejUpdate,
            MatejDelete);
    }
}
