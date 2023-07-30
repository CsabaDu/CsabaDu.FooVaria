namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IFit<T> : IComparable<T>, IEquatable<T> where T : class
{
    bool? FitsIn(T? comparable, LimitMode? limitMode);
}