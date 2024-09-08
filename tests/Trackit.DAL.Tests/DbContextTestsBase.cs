using Microsoft.EntityFrameworkCore;
using Trackit.Common.Tests;
using Trackit.Common.Tests.Factories;
using Xunit;
using Xunit.Abstractions;

namespace Trackit.DAL.Tests
{
    public class DbContextTestsBase : IAsyncLifetime
    {
        protected DbContextTestsBase(ITestOutputHelper output)
        {
            XUnitTestOutputConverter converter = new(output);
            Console.SetOut(converter);

            DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);

            TrackitDbContextSUT = DbContextFactory.CreateDbContext();
        }

        protected IDbContextFactory<TrackitDbContext> DbContextFactory { get; }
        protected TrackitDbContext TrackitDbContextSUT { get; }

        public async Task InitializeAsync()
        {
            await TrackitDbContextSUT.Database.EnsureDeletedAsync();
            await TrackitDbContextSUT.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await TrackitDbContextSUT.Database.EnsureDeletedAsync();
            await TrackitDbContextSUT.DisposeAsync();
        }
    }
}
