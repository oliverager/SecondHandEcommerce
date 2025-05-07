namespace SecondHandEcommerce.Application.Commands.Reviews;

public class CreateReviewCommand
{
    public string SellerId { get; set; } = null!;
    public string Text { get; set; } = null!;
    public double Rating { get; set; }
}
