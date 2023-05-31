namespace hazped.sharedkernel.Primitives;

public abstract class AggregateRootId<TId> : ValueObject
{
    public TId Id { get; protected set; }

    protected AggregateRootId(TId id)
    {
        Id = id;
    }
}