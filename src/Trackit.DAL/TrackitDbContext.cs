using Microsoft.EntityFrameworkCore;
using Trackit.DAL.Entities;
using Trackit.DAL.Seeds;

namespace Trackit.DAL
{
    public class TrackitDbContext : DbContext
    {
        private readonly bool _seedDemoData;

        public TrackitDbContext(DbContextOptions contextOptions, bool seedDemoData = false) : base(contextOptions) => _seedDemoData = seedDemoData;

        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();

        public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<UsersInProjectEntity> UsersInProject => Set<UsersInProjectEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.Project)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectEntity>()
                .HasMany(i => i.Users)
                .WithOne(i => i.Project)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasMany<UsersInProjectEntity>()
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);

            if (_seedDemoData)
            {
                UserSeeds.Seed(modelBuilder);
                ProjectSeeds.Seed(modelBuilder);
                UsersInProjectSeeds.Seed(modelBuilder);
                ActivitySeeds.Seed(modelBuilder);
            }
        }
    }
}
