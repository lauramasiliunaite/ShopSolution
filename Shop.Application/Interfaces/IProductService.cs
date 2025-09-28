using Shop.Application.DTOs;
using Shop.Domain.Entities;

namespace Shop.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int productId);
        Task<Product> CreateAsync(CreateProductDto createProductDto, CancellationToken cancellationToken = default);
        Task<Product?> UpdateAsync(int productId, UpdateProductDto updateProductDto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken = default);
    }
}
