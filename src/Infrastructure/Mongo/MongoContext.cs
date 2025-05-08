using Infrastructure.Mongo.Models;
using MongoDB.Driver;

namespace Infrastructure.Mongo;

public class MongoContext
{
    private readonly IMongoDatabase _db;
    public IMongoClient Client { get; }

    public MongoContext(IMongoDatabase database, IMongoClient client)
    {
        _db = database;
        Client = client;
        EnsureMongoSetup(_db);
    }

    private static async void EnsureMongoSetup(IMongoDatabase db)
    {
        try
        {
            var collectionNames = await db.ListCollectionNames().ToListAsync();

            if (!collectionNames.Contains("Listings"))
            {
                await db.CreateCollectionAsync("Listings");
            }

            if (!collectionNames.Contains("Orders"))
            {
                await db.CreateCollectionAsync("Orders");
            }

            if (!collectionNames.Contains("Users"))
            {
                await db.CreateCollectionAsync("Users");    
            }

            if (!collectionNames.Contains("Reviews"))
            {
                await db.CreateCollectionAsync("Reviews");
            }

            // Repeat as needed
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }
    public IMongoCollection<UserDocument> Users => _db.GetCollection<UserDocument>("Users");
    public IMongoCollection<ListingDocument> Listings => _db.GetCollection<ListingDocument>("Listings");
    public IMongoCollection<OrderDocument> Orders => _db.GetCollection<OrderDocument>("Orders");
    public IMongoCollection<ReviewDocument> Reviews => _db.GetCollection<ReviewDocument>("Reviews");
}