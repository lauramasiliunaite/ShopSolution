using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly ShopDbContext _dbContext;
        public OrderRepository(ShopDbContext dbContext) => _dbContext = dbContext;

        public Task<List<Order>> GetAllAsync() =>
            _dbContext.Orders.Include(order => order.Items).AsNoTracking().ToListAsync();

        public Task AddAsync(Order order) => _dbContext.Orders.AddAsync(order).AsTask();
    }
}