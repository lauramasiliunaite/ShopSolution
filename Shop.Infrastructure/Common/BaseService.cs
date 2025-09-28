using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Common;

public abstract class BaseService
{
    protected readonly ShopDbContext DbContext;

    protected BaseService(ShopDbContext dbContext)
    {
        DbContext = dbContext;
    }

    protected Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}
