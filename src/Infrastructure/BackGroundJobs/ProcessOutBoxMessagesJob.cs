using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence;
using Persistence.Outbox;
using Quartz;

namespace Infrastructure.BackGroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutBoxMessagesJob : IJob
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public ProcessOutBoxMessagesJob(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var messages = await _context.Set<OutBoxMessage>()
                .Where(x => x.ProcessedOnUtc == null)
                .Take(10)
                .ToListAsync(context.CancellationToken);

            foreach (var outboxMessage in messages)
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                if (domainEvent is null)
                {
                    continue;
                }

                await _publisher.Publish(domainEvent, context.CancellationToken);
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync(context.CancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
