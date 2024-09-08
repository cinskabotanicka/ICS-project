using Microsoft.EntityFrameworkCore;

namespace Trackit.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<TrackitDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<TrackitDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}

