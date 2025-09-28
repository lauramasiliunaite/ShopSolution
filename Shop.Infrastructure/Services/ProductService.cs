using Shop.Application.DTOs;
using Shop.Application.Interfaces;
using Shop.Application.Validation;
using Shop.Domain.Entities;
using Shop.Infrastructure.Common;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.Services;

public class ProductService : BaseService, IProductService
{
    private readonly ProductRepository _productRepository;
    private readonly ProductSqlRepository _productSqlRepository;

    public ProductService(
        ShopDbContext dbContext,
        ProductRepository productRepository,
        ProductSqlRepository productSqlRepository)
        : base(dbContext)
    {
        _productRepository = productRepository;
        _productSqlRepository = productSqlRepository;
    }

    public Task<List<Product>> GetAllAsync() => _productSqlRepository.GetAllAsync();

    public Task<Product?> GetByIdAsync(int id) => _productRepository.GetByIdAsync(id);

    public async Task<Product> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        ProductValidator.Validate(dto);

        var newProduct = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity
        };

        await _productRepository.AddAsync(newProduct);
        await SaveChangesAsync(cancellationToken);

        return newProduct;
    }

    public async Task<Product?> UpdateAsync(int id, UpdateProductDto dto, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct is null) return null;

        existingProduct.Name = dto.Name;
        existingProduct.Description = dto.Description;
        existingProduct.Price = dto.Price;
        existingProduct.StockQuantity = dto.StockQuantity;

        _productRepository.Update(existingProduct);
        await SaveChangesAsync(cancellationToken);

        return existingProduct;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct is null) return false;

        _productRepository.Remove(existingProduct);
        await SaveChangesAsync(cancellationToken);

        return true;
    }
}
