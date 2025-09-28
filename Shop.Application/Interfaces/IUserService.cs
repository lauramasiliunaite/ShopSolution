using Shop.Application.DTOs;
using Shop.Domain.Entities;

namespace Shop.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(UserDto userDto, CancellationToken cancellationToken = default);
        Task<User?> LoginAsync(UserDto userDto, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email);
    }
}