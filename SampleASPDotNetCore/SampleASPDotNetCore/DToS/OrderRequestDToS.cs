namespace SampleASPDotNetCore.DToS
{
    public record CreateOrderRequest(
        CustomerInfo Customer,
        decimal TotalAmount,
        DateTime OrderDate,
        List<CreateOrderItemRequest> Items);
    public record CustomerInfo(string Name, string Email);
    public record CreateOrderItemRequest(string ProductName, int Quantity, decimal price);    
    
}
