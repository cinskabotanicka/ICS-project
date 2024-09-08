using Microsoft.EntityFrameworkCore.Design;

namespace Trackit.DAL.Factories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TrackitDbContext>
    {
        private readonly DbContextSqLiteFactory _dbContextSqLiteFactory;
        private const string ConnectionString = $"Data Source=Trackit;Cache=Shared";

        public DesignTimeDbContextFactory()
        {
            _dbContextSqLiteFactory = new DbContextSqLiteFactory(ConnectionString);
        }

        public TrackitDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
    }
}
