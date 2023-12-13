namespace CsabaDu.FooVaria.RateComponents.Behaviors
{
    public interface IConvertMeasure
    {
        internal const decimal ConvertRatio = 1000m;
    }

    public interface IConvertMeasure<TSelf, TOther> : IConvertMeasure where TSelf : class, IMeasure where TOther : notnull
    {
        TSelf ConvertFrom(TOther other);
        TOther ConvertMeasure();
    }
}
