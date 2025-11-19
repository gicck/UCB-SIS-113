using System.Data;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gatherly.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IDbTransaction BeginTransaction()
    {
        var transaction = _dbContext.Database.BeginTransaction();

        // _dbContext.Database.CurrentTransaction;
        // _dbContext.Database.RollbackTransaction();
        // _dbContext.Database.CommitTransaction();
        return transaction.GetDbTransaction();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}