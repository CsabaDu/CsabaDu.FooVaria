namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IConvertMeasure<T, U> where T : class, IMeasure where U : notnull
{
    T ConvertFrom(U other, Enum? measureUnit = null);
    U ConvertMeasure(T? convertibleMeasure = null);
}
