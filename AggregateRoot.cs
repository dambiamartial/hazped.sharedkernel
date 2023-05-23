namespace hazped.sharedkernel;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
{
    public new AggregateRootId<TIdType> Id { get; protected set; }
    protected AggregateRoot(TId id) => Id = id;

#pragma warning disable CS8618
    protected AggregateRoot() { }
#pragma warning restore CS8618

    /// <summary>
    /// Aggretate verion
    /// </summary>
    public long Version { get; private set; } = -1;

    /// <summary>
    /// Aggregate events
    /// </summary>
    private List<IEvent>? _events;

    /// <summary>
    /// Get events
    /// </summary>
    public IReadOnlyCollection<IEvent>? GetEvents => _events?.AsReadOnly();

    /// <summary>
    /// Add event to aggregate events
    /// </summary>
    /// <param name="event"></param>
    public void RaiseEvent(IEvent @event)
    {
        _events ??= new();
        _events.Add(@event);
    }

    /// <summary>
    /// Clear all aggregate events
    /// </summary>
    public void CommitEvents() => _events?.Clear();

    /// <summary>
    /// Remove a specific aggregate event
    /// </summary>
    /// <param name="event"></param>
    public void RemoveEvent(IEvent @event) => _events?.Remove(@event);

    /// <summary>
    /// Apply event to aggregate
    /// </summary>
    /// <param name="event">Evenement courant</param>
    /// <param name="isPrevious">Previously registred event</param>
    public void ApplyEvent(IEvent @event, bool isPrevious = false)
    {
        ((dynamic)this).When((dynamic)@event);

        if (isPrevious)
            return;

        //If is a new event, add event to aggregate events list
        RaiseEvent(@event);
    }

    /// <summary>
    /// Rehydratation de l'agrégat
    /// </summary>
    /// <param name="events">Events list</param>
    public void RehydrateEvents(IEnumerable<IEvent> events)
    {
        foreach (var @event in events)
        {
            //Apply event to aggregate
            ApplyEvent(@event, true);

            //Increment aggregate verion
            Version++;
        }
    }

    /// <summary>
    /// Check the event type to apply to the aggregate
    /// </summary>
    /// <param name="event">Event</param>
    protected abstract void When(IEvent @event);
}