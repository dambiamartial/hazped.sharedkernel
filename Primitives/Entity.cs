namespace hazped.sharedkernel.Primitives;

public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Check the equality from the object to the current object
    /// </summary>
    /// <param name="obj">Object to check the equality</param>
    /// <returns>True if the checked object is equals to the current</returns>
    public override bool Equals(object? obj) => obj is Entity<TId> entity && Id.Equals(entity.Id);

    /// <summary>
    /// check the equality between two entities
    /// </summary>
    /// <param name="left">First entity</param>
    /// <param name="right">Second entity</param>
    /// <returns>True if the 2 entities are equals</returns>
    public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);

    /// <summary>
    /// check the equality between two entities
    /// </summary>
    /// <param name="left">First entity</param>
    /// <param name="right">Second entity</param>
    /// <returns>False if the 2 entities are equals</returns>
    public static bool operator !=(Entity<TId> left, Entity<TId> right) => !Equals(left, right);

    /// <summary>
    /// Check the equality from the object to the current object
    /// </summary>
    /// <param name="obj">Object to check the equality</param>
    /// <returns>True if the checked object is equals to the current</returns>
    public bool Equals(Entity<TId>? other) => Equals((object?)other);

    /// <summary>
    /// Get the entity hash code
    /// </summary>
    /// <returns>hash code</returns>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// Check if is transient entity
    /// </summary>
    /// <returns>true if transient</returns>
    public bool IsTransient() => Id.Equals(default);

    DateTime _createdAt;
    DateTime _lastModifiedAt;

    /// <summary>
    /// Entity's creation date
    /// </summary>
    public virtual DateTime CreatedAt
    {
        get => _createdAt;
        protected set => _createdAt = value != default ? value : DateTime.Now;
    }

    /// <summary>
    /// Entity's last modification date
    /// </summary>
    public virtual DateTime LastModifiedAt
    {
        get => _lastModifiedAt;
        protected set => _lastModifiedAt = value != default ? value : DateTime.Now;
    }
}