﻿namespace Shop.Application.DTOs
{
    public record CreateProductDto
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
    }
}
