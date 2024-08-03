namespace MessageContract;

public record ProductUpdatedEvent
{
    public Guid ProductId { get; set; }

    public DateTime CreatedOnUtc { get; set; }
}
