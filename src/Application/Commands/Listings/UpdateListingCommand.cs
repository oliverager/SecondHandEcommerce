namespace SecondHandEcommerce.Application.Commands.Listings;

public class UpdateListingCommand
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public List<string> Category { get; set; } = new();
    public List<string> ImageUrls { get; set; } = new();
}