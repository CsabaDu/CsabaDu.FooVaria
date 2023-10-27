namespace CsabaDu.FooVaria.Common.Behaviors;

public interface ISum<T> where T : class, IBaseMeasurable, IQuantifiable, ICalculable
{
    T Add(T? other);
    T Subtract(T? other);
}

