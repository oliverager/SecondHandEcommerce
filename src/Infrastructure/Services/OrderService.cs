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

            if (listingDoc == null || listingDoc.Status != ListingStatus.Available)
                throw new InvalidOperationException("Item is not available");
            
            var userDoc = await _context.Users
                .Find(session, x => x.Id == buyerId)
                .FirstOrDefaultAsync();

            if (userDoc == null)
                throw new InvalidOperationException("Buyer does not exist");


            listingDoc.Status = ListingStatus.Reserved;

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
    
    public async Task CancelOrderAsync(string orderId)
    {
        using var session = await _context.Client.StartSessionAsync();
        session.StartTransaction();

        try
        {
            var orderDoc = await _context.Orders
                .Find(session, x => x.Id == orderId)
                .FirstOrDefaultAsync();

            if (orderDoc == null)
                throw new InvalidOperationException("Order not found");

            if (orderDoc.Status != OrderStatus.Pending && orderDoc.Status != OrderStatus.Paid)
                throw new InvalidOperationException("Only pending or paid orders can be cancelled");

            orderDoc.Status = OrderStatus.Cancelled;

            var listingDoc = await _context.Listings
                .Find(session, x => x.Id == orderDoc.ItemId)
                .FirstOrDefaultAsync();

            if (listingDoc != null && listingDoc.Status == ListingStatus.Reserved)
            {
                listingDoc.Status = ListingStatus.Available;
                var listingFilter = Builders<ListingDocument>.Filter.Eq(x => x.Id, listingDoc.Id);
                await _context.Listings.ReplaceOneAsync(session, listingFilter, listingDoc);
            }

            var orderFilter = Builders<OrderDocument>.Filter.Eq(x => x.Id, orderDoc.Id);
            await _context.Orders.ReplaceOneAsync(session, orderFilter, orderDoc);

            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }

}