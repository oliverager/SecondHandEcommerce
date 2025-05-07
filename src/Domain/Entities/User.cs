namespace Domain.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public double Rating { get; set; } = 0.0;

    public List<string> ReviewIds { get; set; } = new();
}
