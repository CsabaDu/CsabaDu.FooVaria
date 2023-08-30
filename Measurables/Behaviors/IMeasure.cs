namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMeasure<T, U, V> where T : class, IMeasure where U : struct where V : struct, Enum
{
    T GetMeasure(U quantity, V measureUnit);
    T GetMeasure(U quantity, string name);
    T GetMeasure(U quantity, IMeasurement? measurement = null);
    T GetMeasure(T? other = null);
    U GetQuantity();
}
