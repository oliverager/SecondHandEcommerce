namespace Domain.Entities;

public class Review
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Text { get; set; } = null!;
    public double Rating { get; set; }
    public string SellerId { get; set; } = null!;
}
