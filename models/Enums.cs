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
        Returned
    }

    public enum PaymentStatus
    {
        Pending,
        Paid, 
        Refunded
    }
}