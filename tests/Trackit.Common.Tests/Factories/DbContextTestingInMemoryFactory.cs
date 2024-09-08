using Trackit.DAL;
using Microsoft.EntityFrameworkCore;

namespace Trackit.Common.Tests.Factories;

public class DbContextTestingInMemoryFactory : IDbContextFactory<TrackitDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextTestingInMemoryFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }

    public TrackitDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TrackitDbContext> contextOptionsBuilder = new();
        contextOptionsBuilder.UseInMemoryDatabase(_databaseName);
        contextOptionsBuilder.EnableSensitiveDataLogging();

        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new TrackitTestingDbContext(contextOptionsBuilder.Options, _seedTestingData);
    }
}