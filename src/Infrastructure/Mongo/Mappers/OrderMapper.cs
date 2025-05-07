using Domain.Entities;
using Infrastructure.Mongo.Models;

namespace Infrastructure.Mongo.Mappers;

public static class OrderMapper
{
    public static OrderDocument ToDocument(Order order) => new()
    {
        Id = order.Id,
        BuyerId = order.BuyerId,
        ItemId = order.ItemId,
        CreatedAt = order.CreatedAt,
        Status = order.Status.ToString()
    };

    public static Order ToDomain(OrderDocument doc) => new()
    {
        Id = doc.Id,
        BuyerId = doc.BuyerId,
        ItemId = doc.ItemId,
        CreatedAt = doc.CreatedAt,
        Status = Enum.Parse<OrderStatus>(doc.Status)
    };
}
