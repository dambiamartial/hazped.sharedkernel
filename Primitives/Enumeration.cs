namespace hazped.sharedkernel.Primitives;

public abstract class Enumeration : IComparable
{
    public string Name { get; private set; }
    public int Id { get; private set; }

    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Select(f => f.GetValue(null)).Cast<T>();

    public override bool Equals(object? obj) => obj is Enumeration otherValue && GetType().Equals(obj.GetType()) && Id.Equals(otherValue.Id);

    public bool Equals(Enumeration? other) => Equals((object?)other);

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Enumeration left, Enumeration right) => Equals(left, right);

    public static bool operator !=(Enumeration left, Enumeration right) => !Equals(left, right);

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue) => Math.Abs(firstValue.Id - secondValue.Id);

    public static bool TryGetFromValueOrName<T>(string valueOrName, out T? enumeration) where T : Enumeration =>
        TryParse(item => item.Name == valueOrName, out enumeration) || int.TryParse(valueOrName, out var value) && TryParse(item => item.Id == value, out enumeration);

    private static bool TryParse<TEnumeration>(Func<TEnumeration, bool> predicate, out TEnumeration? enumeration) where TEnumeration : Enumeration
    {
        enumeration = GetAll<TEnumeration>().FirstOrDefault(predicate);
        return enumeration is not null;
    }

    public static T FromValue<T>(int value) where T : Enumeration => Parse<T, int>(value, "value", item => item.Id == value);

    public static T FromDisplayName<T>(string displayName) where T : Enumeration => Parse<T, string>(displayName, "display name", item => item.Name == displayName);

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        return matchingItem is null
            ? throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}")
            : matchingItem;
    }

    public int CompareTo(object? other) => Id.CompareTo(((Enumeration)other!).Id);
}