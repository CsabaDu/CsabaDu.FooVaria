namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IProportional<T> : IComparable<T>, IEquatable<T>
    where T : class, IMeasurable
{
    decimal ProportionalTo(T comparable);
}