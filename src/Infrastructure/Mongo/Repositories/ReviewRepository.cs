using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Mongo.Mappers;
using Infrastructure.Mongo.Models;
using MongoDB.Driver;

namespace Infrastructure.Mongo.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly IMongoCollection<ReviewDocument> _collection;

    public ReviewRepository(MongoContext context)
    {
        _collection = context.Reviews;
    }

    public async Task<Review?> GetByIdAsync(string id)
    {
        var doc = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return doc == null ? null : ReviewMapper.ToDomain(doc);
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        var docs = await _collection.Find(_ => true).ToListAsync();
        return docs.Select(ReviewMapper.ToDomain);
    }

    public async Task CreateAsync(Review review)
    {
        var doc = ReviewMapper.ToDocument(review);
        await _collection.InsertOneAsync(doc);
    }

    public async Task UpdateAsync(Review review)
    {
        var doc = ReviewMapper.ToDocument(review);
        var filter = Builders<ReviewDocument>.Filter.Eq(x => x.Id, review.Id);
        await _collection.ReplaceOneAsync(filter, doc);
    }

    public async Task DeleteAsync(Review review)
    {
        var filter = Builders<ReviewDocument>.Filter.Eq(x => x.Id, review.Id);
        await _collection.DeleteOneAsync(filter);
    }
}