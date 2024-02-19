namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IFit<TSelf> : IComparable<TSelf>, IEquatable<TSelf>
    where TSelf : class, IBaseQuantifiable
{
    bool? FitsIn(TSelf? other, LimitMode? limitMode);
}