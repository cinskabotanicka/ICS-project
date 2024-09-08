using Microsoft.EntityFrameworkCore;
using Trackit.BL.Mappers;
using Trackit.BL.Mappers.Interfaces;
using Trackit.Common.Tests;
using Trackit.Common.Tests.Factories;
using Trackit.DAL;
using Trackit.DAL.Mappers;
using Trackit.DAL.UnitOfWork;
using Xunit;
using Xunit.Abstractions;

namespace Trackit.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);

        UserEntityMapper = new UserEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();
        ActivityEntityMapper = new ActivityEntityMapper();
        UsersInProjectEntityMapper = new UsersInProjectEntityMapper();

        ActivityModelMapper = new ActivityModelMapper();
        UserModelMapper = new UserModelMapper(ActivityModelMapper);
        UsersInProjectModelMapper = new UsersInProjectModelMapper();
        ProjectModelMapper = new ProjectModelMapper(UsersInProjectModelMapper, ActivityModelMapper);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<TrackitDbContext> DbContextFactory { get; }

    protected UserEntityMapper UserEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected UsersInProjectEntityMapper UsersInProjectEntityMapper { get; }

    protected IUserModelMapper UserModelMapper { get; }
    protected IProjectModelMapper ProjectModelMapper { get; }
    protected IActivityModelMapper ActivityModelMapper { get; }
    protected UsersInProjectModelMapper UsersInProjectModelMapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
