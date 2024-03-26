namespace CsabaDu.FooVaria.Measures.Behaviors
{
    public interface IConvertMeasure;

    public interface IConvertMeasure<TSelf, TOther> : IConvertMeasure, IProportional<TOther>
        where TSelf : class, IMeasure
        where TOther : notnull
    {
        TSelf ConvertFrom(TOther other);
        TOther ConvertMeasure();
    }
}
