using Microsoft.EntityFrameworkCore;
using Trackit.Common.Tests;
using Trackit.DAL.Entities;
using Xunit;
using Xunit.Abstractions;
using Trackit.Common.Tests.Seeds;

namespace Trackit.DAL.Tests;

public class DbContextProjectTests : DbContextTestsBase
{
    public DbContextProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task NewProject_Add_Added()
    {
        var Project = new ProjectEntity()
        {
            Id = Guid.Parse("8189075b-ff09-4641-9209-ecd8dc9220bd"),
            Name = "SampleProject",
        };

        TrackitDbContextSUT.Projects.Add(Project);
        await TrackitDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualProjects = await dbx.Projects.SingleAsync(i => i.Id == Project.Id);
        DeepAssert.Equal(Project, actualProjects);
    }

    [Fact]
    public async Task GetAll_Projects_ContainsSeededSampleProject()
    {
        //Act
        var entities = await TrackitDbContextSUT.Projects.ToArrayAsync();

        //Assert
        DeepAssert.Contains(ProjectSeeds.SampleProject, entities);
    }

    [Fact]
    public async Task GetById_Project_SampleProjectRetrieved()
    {
        //Act
        var entity = await TrackitDbContextSUT.Projects.SingleAsync(i => i.Id == ProjectSeeds.SampleProject.Id);

        //Assert
        DeepAssert.Equal(ProjectSeeds.SampleProject, entity);
    }

    [Fact]
    public async Task Update_Project_Persisted()
    {
        //Arrange
        var baseEntity = ProjectSeeds.SampleProjectUpdate;
        var entity =
            baseEntity with
            {
                Name = baseEntity + "Updated",
            };

        //Act
        TrackitDbContextSUT.Projects.Update(entity);
        await TrackitDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_Project_SampleProjectDeleted()
    {
        //Arrange
        var entityBase = ProjectSeeds.SampleProjectDelete;

        //Act
        TrackitDbContextSUT.Projects.Remove(entityBase);
        await TrackitDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await TrackitDbContextSUT.Projects.AnyAsync(i => i.Id == entityBase.Id));
    }

    [Fact]
    public async Task DeleteById_Project_SampleProjectDeleted()
    {
        //Arrange
        var entityBase = ProjectSeeds.SampleProjectDelete;

        //Act
        TrackitDbContextSUT.Remove(
            TrackitDbContextSUT.Projects.Single(i => i.Id == entityBase.Id));
        await TrackitDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await TrackitDbContextSUT.Projects.AnyAsync(i => i.Id == entityBase.Id));
    }
}