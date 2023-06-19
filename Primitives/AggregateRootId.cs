namespace hazped.sharedkernel.Primitives;

public abstract class AggregateRootId<TId> : ValueObject
{
    public TId Value { get; protected set; }

    protected AggregateRootId(TId value)
    {
        Value = value;
    }
}