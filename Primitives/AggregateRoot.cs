namespace hazped.sharedkernel.Primitives;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
{
    //public new AggregateRootId<TIdType> Id { get; protected set; }

    protected AggregateRoot(TId id) : base(id) => Id = id;

    /// <summary>
    /// Aggregate version
    /// </summary>
    public long Version { get; private set; } = -1;

    /// <summary>
    /// Aggregate events
    /// </summary>
    private List<IDomainEvent>? _domainEvents;

    /// <summary>
    /// Get events
    /// </summary>
    public IReadOnlyCollection<IDomainEvent>? GetDomainEvents => _domainEvents?.AsReadOnly();

    /// <summary>
    /// Add event to aggregate events
    /// </summary>
    /// <param name="event"></param>
    public void RaiseEvent(IDomainEvent @event)
    {
        _domainEvents ??= new();
        _domainEvents.Add(@event);
    }

    /// <summary>
    /// Clear all aggregate events
    /// </summary>
    public void CommitDomainEvents() => _domainEvents?.Clear();

    /// <summary>
    /// Remove a specific domain event
    /// </summary>
    /// <param name="event"></param>
    public void RemoveDomainEvent(IDomainEvent @event) => _domainEvents?.Remove(@event);

    /// <summary>
    /// Apply event to aggregate
    /// </summary>
    /// <param name="event">Current event</param>
    /// <param name="isPrevious">Previously registered event</param>
    public void ApplyEvent(IDomainEvent @event, bool isPrevious = false)
    {
        ((dynamic)this).When((dynamic)@event);

        if (isPrevious)
            return;

        //If is a new event, add event to aggregate events list
        RaiseEvent(@event);
    }

    /// <summary>
    /// Aggregate rehydration
    /// </summary>
    /// <param name="events">Events list</param>
    public void RehydrateEvents(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            //Apply event to aggregate
            ApplyEvent(@event, true);

            //Increment aggregate version
            Version++;
        }
    }

    /// <summary>
    /// Check the event type to apply to the aggregate
    /// </summary>
    /// <param name="event">Event</param>
    protected abstract void When(IDomainEvent @event);
}