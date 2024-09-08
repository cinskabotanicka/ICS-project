using Trackit.BL.Facades;
using Trackit.BL.Models;
using Trackit.Common.Tests;
using Trackit.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Trackit.BL.Facades.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace Trackit.BL.Tests;

public sealed class ProjectFacadeTests : FacadeTestsBase
{
    private readonly IProjectFacade _ProjectFacadeSUT;

    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _ProjectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingProject_DoesNotThrow()
    {
        var model = new ProjectDetailModel()
        {
            Id = Guid.Empty,
            Name = "The Beatles"
        };

        var _ = await _ProjectFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededProject()
    {
        var Projects = await _ProjectFacadeSUT.GetAsync();
        var Project = Projects.Single(i => i.Id == ProjectSeeds.SampleProject.Id);

        DeepAssert.Equal(ProjectModelMapper.MapToListModel(ProjectSeeds.SampleProject), Project);
    }

    [Fact]
    public async Task GetById_SeededProject()
    {
        var Project = await _ProjectFacadeSUT.GetAsync(ProjectSeeds.SampleProject.Id);

        DeepAssert.Equal(ProjectModelMapper.MapToDetailModel(ProjectSeeds.SampleProject), Project);
    }

    [Fact]
    public async Task GetById_NonExistentProject()
    {
        var Project = await _ProjectFacadeSUT.GetAsync(ProjectSeeds.EmptyProject.Id);

        Assert.Null(Project);
    }

    [Fact]
    public async Task DeleteById_SeededProject_Deleted()
    {
        await _ProjectFacadeSUT.DeleteAsync(ProjectSeeds.SampleProject.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Projects.AnyAsync(i => i.Id == ProjectSeeds.SampleProject.Id));
    }

    [Fact]
    public async Task NewProject_InsertOrUpdate_ProjectAdded()
    {
        //Arrange
        var Project = new ProjectDetailModel()
        {
            Id = Guid.Empty,
            Name = "Teambuilding"
        };

        //Act
        Project = await _ProjectFacadeSUT.SaveAsync(Project);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var ProjectFromDb = await dbxAssert.Projects.SingleAsync(i => i.Id == Project.Id);
        DeepAssert.Equal(Project, ProjectModelMapper.MapToDetailModel(ProjectFromDb));
    }

    [Fact]
    public async Task SeededProject_InsertOrUpdate_ProjectUpdated()
    {
        //Arrange
        var Project = new ProjectDetailModel()
        {
            Id = ProjectSeeds.SampleProject.Id,
            Name = ProjectSeeds.SampleProject.Name
        };
        Project.Name += "updated";

        //Act
        await _ProjectFacadeSUT.SaveAsync(Project);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var ProjectFromDb = await dbxAssert.Projects.SingleAsync(i => i.Id == Project.Id);
        DeepAssert.Equal(Project, ProjectModelMapper.MapToDetailModel(ProjectFromDb));
    }
}