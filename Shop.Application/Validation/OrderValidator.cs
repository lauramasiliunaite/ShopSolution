using Shop.Domain.Entities;

namespace Shop.Application.Validation;

public static class OrderValidator
{
    public static bool IsValidOrderItem(Product? product, int quantity)
    {
        return product is not null &&
               quantity > 0 &&
               product.StockQuantity >= quantity;
    }

    public static decimal CalculateTotal(Order order)
    {
        return order.Items.Sum(item => item.Subtotal);
    }
}
