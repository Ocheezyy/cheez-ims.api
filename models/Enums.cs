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
        Processing,
        Shipped,
        Delivered,
        Canceled,
        Returned,
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

    public enum SupplierStatus
    {
        Inactive,
        OnHold,
        New,
        Active
    }
}