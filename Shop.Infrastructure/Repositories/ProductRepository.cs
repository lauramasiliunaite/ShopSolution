using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    public class ProductRepository
    {
        private readonly ShopDbContext _dbContext;
        public ProductRepository(ShopDbContext dbContext) => _dbContext = dbContext;

        public Task<List<Product>> GetAllAsync() => 
            _dbContext.Products.AsNoTracking().ToListAsync();

        public Task<Product?> GetByIdAsync(int ProductId) =>
            _dbContext.Products.FindAsync(ProductId).AsTask();

        public Task AddAsync(Product product) => 
            _dbContext.Products.AddAsync(product).AsTask();

        public void Update(Product product) => _dbContext.Products.Update(product);
        public void Remove(Product product) => _dbContext.Products.Remove(product);
    }
}
