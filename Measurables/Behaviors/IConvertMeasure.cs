namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface IConvertMeasure
    {
    }

    public interface IConvertMeasure<T, X> : IConvertMeasure where T : class, IMeasure where X : notnull
    {
        T ConvertFrom(X other);
        X ConvertMeasure();
    }
}
