namespace CsabaDu.FooVaria.Common.Behaviors;

public interface ISum<T> where T : class, IBaseMeasure, ICalculate
{
    T Add(T? other);
    T Subtract(T? other);
}

