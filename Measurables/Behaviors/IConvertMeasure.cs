namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IConvertMeasure<T, U, V> where T : class, IMeasure where U : notnull where V : struct, Enum
{
    T ConvertFrom(U other, V? measureUnit = null);
    U ConvertMeasure(T? convertibleMeasure = null);
}
