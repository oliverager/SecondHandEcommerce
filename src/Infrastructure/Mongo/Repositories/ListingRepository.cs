using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Mongo.Mappers;
using Infrastructure.Mongo.Models;
using MongoDB.Driver;

namespace Infrastructure.Mongo.Repositories;

public class ListingRepository : IListingRepository
{
    private readonly IMongoCollection<ListingDocument> _collection;

    public ListingRepository(MongoContext context)
    {
        _collection = context.Listings;
    }

    public async Task<Listing?> GetByIdAsync(string id)
    {
        var doc = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return doc is null ? null : ListingMapper.ToDomain(doc);
    }

    public async Task<IEnumerable<Listing>> GetAllAsync()
    {
        var docs = await _collection.Find(_ => true).ToListAsync();
        return docs.Select(ListingMapper.ToDomain);
    }

    public async Task CreateAsync(Listing listing)
    {
        var doc = ListingMapper.ToDocument(listing);
        await _collection.InsertOneAsync(doc);
    }

    public async Task UpdateAsync(Listing listing)
    {
        var doc = ListingMapper.ToDocument(listing);
        await _collection.ReplaceOneAsync(x => x.Id == doc.Id, doc);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}