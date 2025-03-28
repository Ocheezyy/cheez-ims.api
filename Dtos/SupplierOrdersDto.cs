using cheez_ims_api.models;

namespace cheez_ims_api.Dtos;

public class SupplierOrdersDto
{
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShippingAddress { get; set; }
    public decimal TotalAmount { get; set; }
    public Enums.PaymentMethod PaymentMethod { get; set; }
    public Enums.PaymentStatus PaymentStatus { get; set; }
    public Enums.OrderStatus OrderStatus { get; set; }
    public Guid UserId { get; set; }
    public int OrderItemCount { get; set; }
}