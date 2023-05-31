namespace hazped.sharedkernel.Interfaces;

//
// Résumé :
//     Marker interface to represent a root aggregate
public interface IAggregateRoot
{
    public abstract IReadOnlyCollection<IDomainEvent>? GetDomainEvents();
    public abstract void CommitDomainEvents();
}