using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly ShopDbContext _dbContext;
        public UserRepository(ShopDbContext dbContext) => _dbContext = dbContext;

        public Task<User?> GetByEmailAsync(string email) =>
            _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);

        public Task AddAsync(User user) => _dbContext.Users.AddAsync(user).AsTask();
    }
}