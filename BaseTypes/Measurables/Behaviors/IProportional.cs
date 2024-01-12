namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IProportional<TSelf> : IComparable<TSelf>, IEquatable<TSelf>
    where TSelf : class, Types.IMeasurable
{
    decimal ProportionalTo(TSelf comparable);
}