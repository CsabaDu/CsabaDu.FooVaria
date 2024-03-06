namespace CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;

public interface IProportional<TSelf> : IComparable<TSelf>, IEquatable<TSelf>
    where TSelf : class, IMeasurable
{
    decimal ProportionalTo(TSelf? other);
}