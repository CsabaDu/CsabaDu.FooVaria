namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasure<T, U, V> : ICustomMeasure where T : class, IMeasure where U : struct where V : struct, Enum
{
    T GetNextCustomMeasure(U quantity, decimal exchangeRate, string customName);
}

