namespace MessageContract;
public record ProductCreatedEvent
{
    public string ProductName { get; set; } = string.Empty;
    public Guid Id { get; set; }
}
