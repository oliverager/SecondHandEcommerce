using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Mongo.Models;

public class OrderDocument
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; set; } = null!;

    [BsonElement("buyerId")]
    public string BuyerId { get; set; } = null!;

    [BsonElement("itemId")]
    public string ItemId { get; set; } = null!;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("status")]
    public string Status { get; set; } = "Pending";
}
