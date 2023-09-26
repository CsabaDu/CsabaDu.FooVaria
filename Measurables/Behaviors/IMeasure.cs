namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMeasure<T, U, V> where T : class, IMeasure where U : struct where V : struct, Enum
{
    T GetMeasure(IBaseMeasure baseMeasure);
    T GetMeasure(U quantity, V measureUnit);
    T GetMeasure(U quantity, string name);
    T GetMeasure(U quantity);
    T GetMeasure(U quantity, IMeasurement measurement);
    T GetMeasure(T other);
}
