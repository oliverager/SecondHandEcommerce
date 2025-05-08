using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Mongo;
using Infrastructure.Mongo.Mappers;
using Infrastructure.Mongo.Models;
using MongoDB.Driver;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly MongoContext _context;

    public OrderService(MongoContext context)
    {
        _context = context;
    }

    public async Task<string> PlaceOrderAsync(string buyerId, string itemId)
    {
        using var session = await _context.Client.StartSessionAsync();
        session.StartTransaction();

        try
        {
            var listingDoc = await _context.Listings
                .Find(session, x => x.Id == itemId)
                .FirstOrDefaultAsync();

            if (listingDoc == null || listingDoc.Status != "Available")
                throw new InvalidOperationException("Item is not available");

            listingDoc.Status = "Reserved";

            var order = new Order
            {
                BuyerId = buyerId,
                ItemId = itemId,
                Status = OrderStatus.Pending
            };
            var orderDoc = OrderMapper.ToDocument(order);

            var filter = Builders<ListingDocument>.Filter.Eq(x => x.Id, listingDoc.Id);
            await _context.Listings.ReplaceOneAsync(session, filter, listingDoc);
            await _context.Orders.InsertOneAsync(session, orderDoc);

            await session.CommitTransactionAsync();

            return order.Id;
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
}