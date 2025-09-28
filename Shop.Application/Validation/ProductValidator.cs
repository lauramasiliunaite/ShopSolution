using Shop.Application.DTOs;

namespace Shop.Application.Validation;

public static class ProductValidator
{
    public static void Validate(CreateProductDto dto)
    {
        if (dto.Price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        if (dto.StockQuantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative.");
    }
}
