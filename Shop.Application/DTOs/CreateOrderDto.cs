namespace Shop.Application.DTOs
{
    public record CreateOrderDto
    {
        public List<OrderItemDto> Items { get; init; } = new();
    }

    public record OrderItemDto
    {
        public int ProductId { get; init; }
        public int Quantity { get; init; }
    }
}
