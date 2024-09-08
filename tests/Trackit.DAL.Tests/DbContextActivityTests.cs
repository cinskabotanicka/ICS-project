using Trackit.Common.Tests;
using Trackit.Common.Tests.Seeds;
using Xunit.Abstractions;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Trackit.Common.Enums;

namespace Trackit.DAL.Tests
{
    public class DbContextActivityTests : DbContextTestsBase
    {
        public DbContextActivityTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task AddNew_ActivityWithoutUsers_Failed()
        {
            //Arrange
            var entity = ActivitySeeds.EmptyActivity with
            {
                Name = "CS:GO without the boys",
                Description = "Grinding CS:GO before CS2 comes out",
                Type = ActivityType.Hobby,
                ProjectId = ProjectSeeds.ProjectEntity1.Id
            };

            //Act
            TrackitDbContextSUT.Activities.Add(entity);

            //Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await TrackitDbContextSUT.SaveChangesAsync());
        }

        [Fact]
        public async Task AddNew_ActivityWithUsers_Persisted()
        {
            //Arrange
            var entity = ActivitySeeds.EmptyActivity with
            {
                Name = "CS:GO without the boys",
                Description = "Grinding CS:GO before CS2 comes out",
                User = UserSeeds.EmptyUser with 
                {
                    FirstName = "Karel",
                    LastName = "Vémola",
                },
                Project = ProjectSeeds.EmptyProject with
                {
                    Name = "Gaming with Karlos"
                }
            };

            //Act
            TrackitDbContextSUT.Activities.Add(entity);
            await TrackitDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Activities
                .Include(i => i.User)
                .Include(i => i.Project)
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task GetById_Activity()
        {
            //Act
            var entity = await TrackitDbContextSUT.Activities
                .SingleAsync(i => i.Id == ActivitySeeds.MorningRun.Id);

            //Assert
            DeepAssert.Equal(ActivitySeeds.MorningRun, entity);
        }

        [Fact]
        public async Task Update_Activity_Persisted()
        {
            //Arrange
            var baseEntity = ActivitySeeds.MorningRunUpdate;
            var entity =
                baseEntity with
                {
                    Name = baseEntity.Name + "Updated",
                    Description = baseEntity.Description + "Updated",
                    Type = ActivityType.School,
                };

            //Act
            TrackitDbContextSUT.Activities.Update(entity);
            await TrackitDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Delete_Activity_Deleted()
        {
            //Arrange
            var baseEntity = ActivitySeeds.MorningRunDelete;

            //Act
            TrackitDbContextSUT.Activities.Remove(baseEntity);
            await TrackitDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TrackitDbContextSUT.Activities.AnyAsync(i => i.Id == baseEntity.Id));
        }

        [Fact]
        public async Task DeleteById_Activity_Deleted()
        {
            //Arrange
            var baseEntity = ActivitySeeds.MorningRunDelete;

            //Act
            TrackitDbContextSUT.Remove(
                TrackitDbContextSUT.Activities.Single(i => i.Id == baseEntity.Id));
            await TrackitDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TrackitDbContextSUT.Activities.AnyAsync(i => i.Id == baseEntity.Id));
        }
    }
}
