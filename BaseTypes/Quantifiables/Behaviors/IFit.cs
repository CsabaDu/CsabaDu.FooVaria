namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

public interface IFit<TSelf> : IComparable<TSelf>, IEquatable<TSelf>
    where TSelf : class, IQuantifiable
{
    bool? FitsIn(TSelf? comparable, LimitMode? limitMode);
}