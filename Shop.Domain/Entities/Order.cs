namespace Shop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }
        public int? UserId { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
