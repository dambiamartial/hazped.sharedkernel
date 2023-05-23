namespace hazped.sharedkernel;

public abstract class Entity<TId>
{
    int? _requestedHashCode;
    Guid _id;
    DateTime _createdAt;
    DateTime _lastModifiedAt;

    /// <summary>
    /// Entity's Id
    /// </summary>
    //public virtual Guid Id
    //{
    //    get => _id;
    //    protected set => _id = value != Guid.Empty ? value : Guid.NewGuid();
    //}
    public TId Id { get; protected set; }

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

    /// <summary>
    /// Get the entity hash code
    /// </summary>
    /// <returns>hash code</returns>
    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            _requestedHashCode = !_requestedHashCode.HasValue ? this.Id.GetHashCode() ^ 31 : _requestedHashCode; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();
    }

    /// <summary>
    /// Check if is transient entity
    /// </summary>
    /// <returns>true if transient</returns>
    public bool IsTransient() => this.Id.Equals( default);

    /// <summary>
    /// Check the equality to the objet to the current object
    /// </summary>
    /// <param name="obj">Object to check the equatily</param>
    /// <returns>True if the checked object is iqual to the current</returns>
    public override bool Equals(object obj)
    {
        if (obj == null || obj is not Entity<TId>)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        Entity<TId> item = (Entity<TId>)obj;

        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            return item.Id.Equals(this.Id);
    }

    /// <summary>
    /// check the equality between two entities
    /// </summary>
    /// <param name="left">First entity</param>
    /// <param name="right">Second entity</param>
    /// <returns>True if the 2 entities are iquals</returns>
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        if (Object.Equals(left, null))
            return Equals(right, null);
        else
            return left.Equals(right);
    }

    /// <summary>
    /// check the equality between two entities
    /// </summary>
    /// <param name="left">First entity</param>
    /// <param name="right">Second entity</param>
    /// <returns>False if the 2 entities are iquals</returns>
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }
}