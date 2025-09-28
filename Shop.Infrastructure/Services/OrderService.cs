using Microsoft.EntityFrameworkCore;
using Shop.Application.DTOs;
using Shop.Application.Interfaces;
using Shop.Application.Validation;
using Shop.Domain.Entities;
using Shop.Infrastructure.Common;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.Services;

public class OrderService : BaseService, IOrderService
{
    private readonly ProductRepository _productRepository;
    private readonly OrderRepository _orderRepository;

    public OrderService(
        ShopDbContext dbContext,
        ProductRepository productRepository,
        OrderRepository orderRepository)
        : base(dbContext)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public Task<List<Order>> GetAllAsync() => _orderRepository.GetAllAsync();

    public async Task<Order?> CreateAsync(CreateOrderDto orderDto, int? userId, CancellationToken cancellationToken = default)
    {
        await using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var newOrder = new Order
            {
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            foreach (var orderItemDto in orderDto.Items)
            {
                var product = await _productRepository.GetByIdAsync(orderItemDto.ProductId);
                if (!OrderValidator.IsValidOrderItem(product, orderItemDto.Quantity))
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return null;
                }

                product!.StockQuantity -= orderItemDto.Quantity;
                DbContext.Products.Update(product);

                newOrder.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = orderItemDto.Quantity
                });
            }

            newOrder.TotalPrice = OrderValidator.CalculateTotal(newOrder);

            await _orderRepository.AddAsync(newOrder);
            await SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return newOrder;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
