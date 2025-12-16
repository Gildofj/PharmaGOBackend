namespace PharmaGO.Core.Common;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected static bool EqualOperador(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null)) return false;
        return ReferenceEquals(left, right) || left.Equals(right);
    }

    protected abstract IEnumerable<object?> GetAtomicValues();

    public bool Equals(ValueObject? other) =>
        other is not null && GetAtomicValues().SequenceEqual(other.GetAtomicValues());

    public override bool Equals(object? obj) => Equals(obj as ValueObject);

    public override int GetHashCode() => GetAtomicValues()
        .Select(x => x != null ? x.GetHashCode() : 0)
        .Aggregate((x, y) => (x * 397) ^ y);
    
    public static bool operator ==(ValueObject left, ValueObject right) => EqualOperador(left, right);
    public static bool operator !=(ValueObject left, ValueObject right) => !EqualOperador(left, right);
}