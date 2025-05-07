using Infrastructure.Mongo.Models;
using MongoDB.Driver;

namespace Infrastructure.Mongo;

public class MongoContext
{
    private readonly IMongoDatabase _db;

    public MongoContext(IMongoDatabase database)
    {
        _db = database;
    }

    public IMongoCollection<UserDocument> Users => _db.GetCollection<UserDocument>("Users");
    public IMongoCollection<ListingDocument> Listings => _db.GetCollection<ListingDocument>("Listings");
    public IMongoCollection<OrderDocument> Orders => _db.GetCollection<OrderDocument>("Orders");
    public IMongoCollection<ReviewDocument> Reviews => _db.GetCollection<ReviewDocument>("Reviews");
}
