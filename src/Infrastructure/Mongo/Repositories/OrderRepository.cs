using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Mongo.Mappers;
using Infrastructure.Mongo.Models;
using MongoDB.Driver;

namespace Infrastructure.Mongo.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<OrderDocument> _collection;

    public OrderRepository(MongoContext context)
    {
        _collection = context.Orders;
    }

    public async Task<Order?> GetByIdAsync(string id)
    {
        var doc = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return doc is null ? null : OrderMapper.ToDomain(doc);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var docs = await _collection.Find(_ => true).ToListAsync();
        return docs.Select(OrderMapper.ToDomain);
    }

    public async Task CreateAsync(Order order)
    {
        var doc = OrderMapper.ToDocument(order);
        await _collection.InsertOneAsync(doc);
    }

    public async Task UpdateAsync(Order order)
    {
        var doc = OrderMapper.ToDocument(order);
        await _collection.ReplaceOneAsync(x => x.Id == doc.Id, doc);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
