namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IFit<TSelf> : IComparable<TSelf>, IEquatable<TSelf>
    where TSelf : class, IMeasurable, IQuantifiable
{
    bool? FitsIn(TSelf? comparable, LimitMode? limitMode);
}