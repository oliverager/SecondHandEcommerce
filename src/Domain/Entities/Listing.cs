namespace Domain.Entities;

public enum ListingStatus
{
    Available,
    Reserved,
    Sold,
    Inactive
}

public class Listing
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string SellerId { get; set; } = null!;
    public List<string> Category { get; set; } = new();
    public List<string> ImageUrls { get; set; } = new();
    public ListingStatus Status { get; set; } = ListingStatus.Available;
}
