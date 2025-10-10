using MassTransit;

public class ItemCreatedConsumer : IConsumer<ItemCreatedEvent>
{
    public Task Consume(ConsumeContext<ItemCreatedEvent> context)
    {
        var msg = context.Message;
        Console.WriteLine($"Received Item: {msg.ItemName}");
        return Task.CompletedTask;
    }
}
