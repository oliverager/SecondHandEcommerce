namespace Application.Interfaces;

public interface IOrderService
{
    Task<string> PlaceOrderAsync(string buyerId, string itemId);
    Task CancelOrderAsync(string orderId, string userId);
    
}