using Microsoft.EntityFrameworkCore;
using Trackit.Common.Tests.Seeds;
using Xunit.Abstractions;
using Xunit;
using Trackit.Common.Tests;
using Trackit.DAL.Entities;

namespace Trackit.DAL.Tests
{
    public class DbContextUsersInProjectTests : DbContextTestsBase
    {
        public DbContextUsersInProjectTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task NewUserInProject_Add_Added()
        {
            var UserInProject = new UsersInProjectEntity()
            {
                Id = Guid.Parse("7c28f3c5-fcaf-454d-b430-043f0e3721dd"),
                UserId = UserSeeds.Matej.Id,
                ProjectId = ProjectSeeds.SampleProject.Id,
            };

            TrackitDbContextSUT.UsersInProject.Add(UserInProject);
            await TrackitDbContextSUT.SaveChangesAsync();

            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.UsersInProject.SingleAsync(i => i.Id == UserInProject.Id);
            DeepAssert.Equal(UserInProject, actualEntity);
        }

        [Fact]
        public async Task GetAll_UsersInProjects_ForUser()
        {
            //Act
            var UsersInProjects = await TrackitDbContextSUT.UsersInProject
                .Where(i => i.UserId == UserSeeds.UserEntity1.Id)
                .ToArrayAsync();

            //Assert
            Assert.Contains(UsersInProjectSeeds.UsersInProjectEntity1 with { User = null, Project = null }, UsersInProjects);
            Assert.Contains(UsersInProjectSeeds.UsersInProjectEntity2 with { User = null, Project = null }, UsersInProjects);
        }

        [Fact]
        public async Task Update_UsersInProject_Persisted()
        {
            //Arrange
            var baseEntity = UsersInProjectSeeds.UsersInProjectEntityUpdate;
            var entity =
                baseEntity with
                {
                    ProjectId = ProjectSeeds.ProjectEntity1.Id
                };

            //Act
            TrackitDbContextSUT.UsersInProject.Update(entity);
            await TrackitDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.UsersInProject.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Delete_UsersInProject_Deleted()
        {
            //Arrange
            var baseEntity = UsersInProjectSeeds.UsersInProjectEntityDelete;

            //Act
            TrackitDbContextSUT.UsersInProject.Remove(baseEntity);
            await TrackitDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TrackitDbContextSUT.UsersInProject.AnyAsync(i => i.Id == baseEntity.Id));
        }

        [Fact]
        public async Task DeleteById_UsersInProject_Deleted()
        {
            //Arrange
            var baseEntity = UsersInProjectSeeds.UsersInProjectEntityDelete;

            //Act
            TrackitDbContextSUT.Remove(
                TrackitDbContextSUT.UsersInProject.Single(i => i.Id == baseEntity.Id));
            await TrackitDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TrackitDbContextSUT.Users.AnyAsync(i => i.Id == baseEntity.Id));
        }
    }
}
