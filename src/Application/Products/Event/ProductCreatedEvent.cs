namespace Application.Products.Event;
public record ProductCreatedEvent
{
    public string ProductName { get; set; } = string.Empty;
    public Guid Id { get; set; }
}
