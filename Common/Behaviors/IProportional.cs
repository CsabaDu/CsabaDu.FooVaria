namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IProportional<T> : /*IExchangeable<T, T>, */IComparable<T>, IEquatable<T> where T : class /*where U : notnull*/
{
    decimal ProportionalTo(T comparable);
}