using Microsoft.EntityFrameworkCore;
using Trackit.DAL;

namespace Trackit.Common.Tests.Factories
{
    public class DbContextLocalDBTestingFactory : IDbContextFactory<TrackitDbContext>
    {
        private readonly string _databaseName;
        private readonly bool _seedTestingData;

        public DbContextLocalDBTestingFactory(string databaseName, bool seedTestingData = false)
        {
            _databaseName = databaseName;
            _seedTestingData = seedTestingData;
        }

        public TrackitDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<TrackitDbContext> builder = new();
            builder.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = {_databaseName};MultipleActiveResultSets = True;Integrated Security = True; ");
            builder.EnableSensitiveDataLogging();


            // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
            // builder.EnableSensitiveDataLogging();

            return new TrackitTestingDbContext(builder.Options, _seedTestingData);
        }
    }
}
