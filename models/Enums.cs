namespace cheez_ims_api.models;

public static class Enums
{
    public enum PaymentMethod
    {
        Cash,
        CreditCard,
        Bitcoin
    }

    public enum OrderStatus
    {
        Pending,
        Shipped,
        Delivered,
        Canceled,
        Returned,
        Processing
    }

    public enum PaymentStatus
    {
        Pending,
        Paid, 
        Refunded
    }

    public enum ActivityType
    {
        CreateOrder,
        CreateProduct,
        RestockProduct,
        LowStockProduct,
        CreateSupplier,
        ShippedOrder,
    }

    public enum ProductStatus
    {
        InStock,
        LowStock,
        OutOfStock,
        Discontinued,
    }
}