using Shop.Application.DTOs;
using Shop.Domain.Entities;

namespace Shop.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> CreateAsync(CreateOrderDto createOrderDto, int? userId, CancellationToken cancellationToken = default);
        Task<List<Order>> GetAllAsync();
    }
}
