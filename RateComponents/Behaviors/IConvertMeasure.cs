namespace CsabaDu.FooVaria.RateComponents.Behaviors
{
    public interface IConvertMeasure
    {
    }

    public interface IConvertMeasure<TSelf, TOther> : IConvertMeasure where TSelf : class, IMeasure where TOther : notnull
    {
        TSelf ConvertFrom(TOther other);
        TOther ConvertMeasure();
    }
}
