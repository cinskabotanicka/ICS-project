using Microsoft.EntityFrameworkCore;
using Trackit.Common.Tests.Seeds;
using Trackit.DAL;

namespace Trackit.Common.Tests
{
    public class TrackitTestingDbContext : TrackitDbContext
    {
        private readonly bool _seedTestingData;

        public TrackitTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false) : base(contextOptions, seedDemoData:false)
        {
            _seedTestingData = seedTestingData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            if (!_seedTestingData) return;
            UserSeeds.Seed(modelBuilder);
            ProjectSeeds.Seed(modelBuilder);
            UsersInProjectSeeds.Seed(modelBuilder);
            ActivitySeeds.Seed(modelBuilder);
        }
    }
}
