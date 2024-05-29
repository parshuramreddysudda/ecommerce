namespace ECommerce.Api.Orders.DB;

public class Orders
{
    public int Id { get; set; }
    public DateOnly DateOnly { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string PaymentMethod { get; set; }
    public string ShippingAddress { get; set; }
    public decimal BillAmount { get; set; }
    public List<OrderItem> Items { get; set; }
}