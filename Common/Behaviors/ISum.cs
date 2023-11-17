namespace CsabaDu.FooVaria.Common.Behaviors;

public interface ISum<T> where T : class, IBaseMeasureTemp, ICalculate
{
    T Add(T? other);
    T Subtract(T? other);
}

