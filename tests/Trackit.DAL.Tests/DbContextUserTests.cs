using Microsoft.EntityFrameworkCore;
using Trackit.Common.Tests;
using Trackit.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Trackit.Common.Tests.Seeds;

namespace Trackit.DAL.Tests;

public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task NewUser_Add_Added()
    {
        var user = new UserEntity()
        {
            Id = Guid.Parse("b187bda8-53a3-4fc6-8b00-88e177e207c6"),
            FirstName = "Matej",
            LastName = "Rusinak",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b4/Lionel-Messi-Argentina-2022-FIFA-World-Cup_%28cropped%29.jpg"
        };

        TrackitDbContextSUT.Users.Add(user);
        await TrackitDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualUsers = await dbx.Users.SingleAsync(i=>i.Id == user.Id);
        DeepAssert.Equal(user, actualUsers);
    }

    [Fact]
    public async Task GetAll_Users_ContainsSeededMatej()
    {
        //Act
        var entities = await TrackitDbContextSUT.Users.ToArrayAsync();

        //Assert
        DeepAssert.Contains(UserSeeds.Matej, entities);
    }

    [Fact]
    public async Task GetById_User_MatejRetrieved()
    {
        //Act
        var entity = await TrackitDbContextSUT.Users.SingleAsync(i => i.Id == UserSeeds.Matej.Id);

        //Assert
        DeepAssert.Equal(UserSeeds.Matej, entity);
    }

    [Fact]
    public async Task Update_User_Persisted()
    {
        //Arrange
        var baseEntity = UserSeeds.MatejUpdate;
        var entity =
            baseEntity with
            {
                FirstName = baseEntity + "Updated",
                LastName = baseEntity + "Updated",
            };

        //Act
        TrackitDbContextSUT.Users.Update(entity);
        await TrackitDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_User_MatejDeleted()
    {
        //Arrange
        var entityBase = UserSeeds.MatejDelete;

        //Act
        TrackitDbContextSUT.Users.Remove(entityBase);
        await TrackitDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await TrackitDbContextSUT.Users.AnyAsync(i => i.Id == entityBase.Id));
    }

    [Fact]
    public async Task DeleteById_User_MatejDeleted()
    {
        //Arrange
        var entityBase = UserSeeds.MatejDelete;

        //Act
        TrackitDbContextSUT.Remove(
            TrackitDbContextSUT.Users.Single(i => i.Id == entityBase.Id));
        await TrackitDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await TrackitDbContextSUT.Users.AnyAsync(i => i.Id == entityBase.Id));
    }
}