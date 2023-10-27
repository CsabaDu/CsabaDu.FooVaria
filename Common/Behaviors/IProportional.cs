namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IProportional<T> : IComparable<T>, IEquatable<T> where T : class, IBaseMeasurable
{
    decimal ProportionalTo(T comparable);
}