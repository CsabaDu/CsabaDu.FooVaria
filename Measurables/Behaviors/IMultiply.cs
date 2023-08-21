namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMultiply<in U, out T> : ICalculable where U : notnull where T : class, IMeasurable, ICalculable
{
    T Multiply(U multiplier);
}
