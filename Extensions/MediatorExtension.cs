namespace hazped.sharedkernel.Extensions;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync<TId, TIdType>(this IPublisher mediator, DbContext dbContext) where TId : AggregateRootId<TIdType>
    {
        if (dbContext is null) return;

        //Get hold of all the various entities
        var entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<AggregateRoot<TId, TIdType>>()
            .Where(entry => entry.Entity.GetDomainEvents is not null && entry.Entity.GetDomainEvents.Count != 0)
            .Select(entry => entry.Entity).ToList();

        //Get hold of all the various domain events
        var domainEvents = entitiesWithDomainEvents.SelectMany(entry => entry.GetDomainEvents!).ToList();

        //Clear domain events
        entitiesWithDomainEvents.ForEach(entity => entity.CommitDomainEvents());

        //Publish domain events
        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}