using Trackit.BL.Facades;
using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Models;
using Trackit.Common.Enums;
using Trackit.Common.Tests;
using Trackit.Common.Tests.Seeds;
using Trackit.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Trackit.BL.Tests;

public class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _facadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task Create_WithoutUserAndProject_EqualsCreated()
    {
        //Arrange
        var model = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            UserId = UserSeeds.UserEntity1.Id,
            ProjectId = ProjectSeeds.ProjectEntity1.Id,
            User = null,
            Project = null,
            Name = "Activity 1",
            Description = "Testing Activity 1",
            Type = ActivityType.Housework,
            Start = DateTime.MinValue,
            End = DateTime.MaxValue
        };

        //Act
        var returnedModel = await _facadeSUT.SaveAsync(model);

        //Assert
        DeepAssert.Equal(model with { Id = returnedModel.Id }, returnedModel);
    }

    [Fact]
    public async Task Create_WithUserAndProject_EqualsCreated()
    {
        //Arrange
        var model = new ActivityDetailModel()
        {
            UserId = Guid.Empty,
            ProjectId = Guid.Empty,
            User = new UserEntity()
            {
                Id = UserSeeds.Matej.Id,
                FirstName = UserSeeds.Matej.FirstName,
                LastName = UserSeeds.Matej.LastName,
                PhotoUrl = UserSeeds.Matej.PhotoUrl
            },
            Project = new ProjectEntity()
            {
                Id = ProjectSeeds.ProjectEntity1.Id,
                Name = ProjectSeeds.ProjectEntity1.Name,
            },
            Name = "Activity 5",
            Description = "Testing Activity 8",
            Type = ActivityType.Housework,
            Start = DateTime.MinValue,
            End = DateTime.MaxValue
        };

        //Act && Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _facadeSUT.SaveAsync(model));
    }

    [Fact]
    public async Task Create_WithProjectAndNotUser_Throws()
    {
        //Arrange
        var model = new ActivityDetailModel()
        {
            UserId = Guid.Empty,
            ProjectId = Guid.Empty,
            User = new UserEntity()
            {
                Id = Guid.Parse("b187bda8-53a3-4fc6-8b00-88e177e204c6"),
                FirstName = "Matej",
                LastName = "Rusinak",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b4/Lionel-Messi-Argentina-2022-FIFA-World-Cup_%28cropped%29.jpg"
            },
            Project = new ProjectEntity()
            {
                Id = ProjectSeeds.ProjectEntity1.Id,
                Name = ProjectSeeds.ProjectEntity1.Name,
            },
            Name = "Activity 1",
            Description = "Testing Activity 1",
            Type = ActivityType.Housework,
            Start = DateTime.MinValue,
            End = DateTime.MaxValue
        };

        //Act & Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _facadeSUT.SaveAsync(model));
    }

    [Fact]
    public async Task GetById_FromSeeded_EqualsSeeded()
    {
        //Arrange
        var detailModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.MorningRun);

        //Act
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);

        //Assert
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_FromSeeded_ContainsSeeded()
    {
        //Arrange
        var listModel = ActivityModelMapper.MapToListModel(ActivitySeeds.MorningRun);

        //Act
        var returnedModel = await _facadeSUT.GetAsync();

        //Assert
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Update_FromSeeded_DoesNotThrow()
    {
        //Arrange
        var detailModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity1);
        detailModel.Name = "Changed Activity name";

        //Act & Assert
        await _facadeSUT.SaveAsync(detailModel with { User = default, Project = default });
    }

    [Fact]
    public async Task Update_Name_FromSeeded_CheckUpdated()
    {
        //Arrange
        var detailModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity1);
        detailModel.Name = "Changed Activity name 1";

        //Act
        await _facadeSUT.SaveAsync(detailModel with { User = default, Project = default });

        //Assert
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task Update_RemoveUsers_FromSeeded_CheckNotUpdated()
    {
        //Arrange
        var detailModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity2);
        detailModel.User = UserSeeds.EmptyUser;

        //Act
        await _facadeSUT.SaveAsync(detailModel);

        //Assert
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity2), returnedModel);
    }

    [Fact]
    public async Task DeleteById_FromSeeded_DoesNotThrow()
    {
        //Arrange & Act & Assert
        await _facadeSUT.DeleteAsync(ActivitySeeds.MorningRun.Id);
    }

    [Fact]
    public void GetByUserId_FromSeeded_ContainsSeeded()
    {
        //Arrange & Act & Assert
        var returnedModel = _facadeSUT.GetByUserId(UserSeeds.UserEntity1.Id);
        Assert.Contains(ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityEntity1), returnedModel);
    }

    [Fact]
    public void GetByUserId_FromSeeded_DoesNotContain()
    {
        //Arrange & Act & Assert
        var returnedModel = _facadeSUT.GetByUserId(UserSeeds.EmptyUser.Id);
        Assert.True(!returnedModel.Any());
    }

    [Fact]
    public void GetByProjectId_FromSeeded_ContainsSeeded()
    {
        //Arrange & Act & Assert
        var returnedModel = _facadeSUT.GetByProjectId(ProjectSeeds.ProjectEntity2.Id);
        Assert.Contains(ActivityModelMapper.MapToListModel(ActivitySeeds.ActivityEntity1), returnedModel);
    }

    [Fact]
    public void GetByProjectId_FromSeeded_DoesNotContain()
    {
        //Arrange & Act & Assert
        var returnedModel = _facadeSUT.GetByProjectId(ProjectSeeds.EmptyProject.Id);
        Assert.True(!returnedModel.Any());
    }

}
