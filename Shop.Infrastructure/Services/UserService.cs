using Shop.Application.DTOs;
using Shop.Application.Interfaces;
using Shop.Application.Security;
using Shop.Application.Validation;
using Shop.Domain.Entities;
using Shop.Infrastructure.Common;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.Services;

public class UserService : BaseService, IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(ShopDbContext dbContext, UserRepository userRepository)
        : base(dbContext)
    {
        _userRepository = userRepository;
    }

    public async Task<User> RegisterAsync(UserDto dto, CancellationToken cancellationToken = default)
    {
        UserValidator.Validate(dto);

        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User already exists.");

        var newUser = new User
        {
            Email = dto.Email,
            PasswordHash = PasswordHasher.Hash(dto.Password)
        };

        await _userRepository.AddAsync(newUser);
        await SaveChangesAsync(cancellationToken);

        return newUser;
    }

    public async Task<User?> LoginAsync(UserDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            return null;

        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null) return null;

        return user.PasswordHash == PasswordHasher.Hash(dto.Password) ? user : null;
    }

    public Task<User?> GetByEmailAsync(string email) => _userRepository.GetByEmailAsync(email);
}
