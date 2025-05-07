namespace SecondHandEcommerce.Application.Commands.Listings;

public class CreateListingCommand
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string SellerId { get; set; } = null!;
    public List<string> Category { get; set; } = new();
    public List<string> ImageUrls { get; set; } = new();
}
