using Trackit.BL.Facades;
using Trackit.BL.Models;
using Trackit.Common.Tests;
using Trackit.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Trackit.BL.Facades.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace Trackit.BL.Tests;

public sealed class UserFacadeTests : FacadeTestsBase
{
    private readonly IUserFacade _UserFacadeSUT;

    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _UserFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingUser_DoesNotThrow()
    {
        var model = new UserDetailModel()
        {
            Id = Guid.Empty,
            FirstName = @"Bartolomeo",
            LastName = @"Diaz",
        };

        var _ = await _UserFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededMatej()
    {
        var Users = await _UserFacadeSUT.GetAsync();
        var User = Users.Single(i => i.Id == UserSeeds.Matej.Id);

        DeepAssert.Equal(UserModelMapper.MapToListModel(UserSeeds.Matej), User);
    }

    [Fact]
    public async Task GetById_SeededMatej()
    {
        var User = await _UserFacadeSUT.GetAsync(UserSeeds.Matej.Id);

        DeepAssert.Equal(UserModelMapper.MapToDetailModel(UserSeeds.Matej), User);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var User = await _UserFacadeSUT.GetAsync(UserSeeds.EmptyUser.Id);

        Assert.Null(User);
    }

    [Fact]
    public async Task DeleteById_SeededMatej_Deleted()
    {
        await _UserFacadeSUT.DeleteAsync(UserSeeds.Matej.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == UserSeeds.Matej.Id));
    }

    [Fact]
    public async Task NewUser_InsertOrUpdate_UserAdded()
    {
        //Arrange
        var User = new UserDetailModel()
        {
            Id = Guid.Empty,
            FirstName = "Karel",
            LastName = "Gott",
        };

        //Act
        User = await _UserFacadeSUT.SaveAsync(User);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var UserFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == User.Id);
        DeepAssert.Equal(User, UserModelMapper.MapToDetailModel(UserFromDb));
    }

    [Fact]
    public async Task SeededMatej_InsertOrUpdate_UserUpdated()
    {
        //Arrange
        var User = new UserDetailModel()
        {
            Id = UserSeeds.Matej.Id,
            FirstName = UserSeeds.Matej.FirstName,
            LastName = UserSeeds.Matej.LastName,
        };
        User.FirstName += "updated";
        User.LastName += "updated";

        //Act
        await _UserFacadeSUT.SaveAsync(User);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var UserFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == User.Id);
        DeepAssert.Equal(User, UserModelMapper.MapToDetailModel(UserFromDb));
    }
}