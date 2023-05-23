namespace hazped.sharedkernel.Extensions;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.GetEvents != null && x.Entity.GetEvents.Count != 0);

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.GetEvents!)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.CommitEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}