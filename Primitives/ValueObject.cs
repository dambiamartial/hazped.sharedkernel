namespace hazped.sharedkernel.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// Check the equality from the object to the current object
    /// </summary>
    /// <param name="obj">Object to check the equality</param>
    /// <returns>True if the checked object is equals to the current</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    /// <summary>
    /// check the equality between two entities
    /// </summary>
    /// <param name="left">First entity</param>
    /// <param name="right">Second entity</param>
    /// <returns>True if the 2 entities are equals</returns>
    public static bool operator ==(ValueObject left, ValueObject right) => Equals(left, right);

    /// <summary>
    /// check the equality between two entities
    /// </summary>
    /// <param name="left">First entity</param>
    /// <param name="right">Second entity</param>
    /// <returns>False if the 2 entities are equals</returns>
    public static bool operator !=(ValueObject left, ValueObject right) => !Equals(left, right);

    /// <summary>
    /// Get the value object hash code
    /// </summary>
    /// <returns>hash code</returns>
    public override int GetHashCode() => GetEqualityComponents().Select(x => x?.GetHashCode() ?? 0).Aggregate((x, y) => x ^ y);

    /// <summary>
    /// Check the equality from the object to the current object
    /// </summary>
    /// <param name="other">Object to check the equality</param>
    /// <returns>True if the checked object is equals to the current</returns>
    public bool Equals(ValueObject? other) => Equals((object?)other);

    public ValueObject? GetCopy() => MemberwiseClone() as ValueObject;

}