namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IConvertMeasure<T, U> : IConvertMeasure where T : class, IMeasure where U : notnull
{
    T ConvertFrom(U other);
    U ConvertMeasure();
}
