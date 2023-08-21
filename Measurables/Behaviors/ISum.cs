namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ISum<T> : ICalculable where T : class, IMeasurable, ICalculable
{
    T Add(T? other);
    T Subtract(T? other);
}

