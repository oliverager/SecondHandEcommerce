using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Mongo.Mappers;
using Infrastructure.Mongo.Models;
using MongoDB.Driver;

namespace Infrastructure.Mongo.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<UserDocument> _collection;

    public UserRepository(MongoContext context)
    {
        _collection = context.Users;
        CreateIndexes();
    }


    private void CreateIndexes()
{
    var indexKeys = Builders<UserDocument>.IndexKeys;

    var emailIndex = new CreateIndexModel<UserDocument>(
        indexKeys.Ascending(x => x.Email),
        new CreateIndexOptions { Unique = true });

    // Optional: Uncomment if usernames are part of your model
    /*
    var usernameIndex = new CreateIndexModel<UserDocument>(
        indexKeys.Ascending(x => x.Username),
        new CreateIndexOptions { Unique = true });
    */

    _collection.Indexes.CreateOne(emailIndex);
    // _collection.Indexes.CreateOne(usernameIndex);
}


    public async Task<User?> GetByIdAsync(string id)
    {
        var doc = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return doc is null ? null : UserMapper.ToDomain(doc);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var docs = await _collection.Find(_ => true).ToListAsync();
        return docs.Select(UserMapper.ToDomain);
    }

    public async Task CreateAsync(User user)
    {
        var doc = UserMapper.ToDocument(user);
        await _collection.InsertOneAsync(doc);
    }

    public async Task UpdateAsync(User user)
    {
        var doc = UserMapper.ToDocument(user);
        await _collection.ReplaceOneAsync(x => x.Id == doc.Id, doc);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}