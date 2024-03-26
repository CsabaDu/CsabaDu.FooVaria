namespace CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;

public interface IProportional<TComparable> : IComparable<TComparable>, IEquatable<TComparable>
    where TComparable : /*class, IMeasurable*/ notnull
{
    decimal ProportionalTo(TComparable? other);
}