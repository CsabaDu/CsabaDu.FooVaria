namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IMeasure : IRateComponent, ILimitable, ICalculate<decimal, IMeasure>
    {
    }

    public interface IMeasure<TSelf, TNum> : IMeasure, IRateComponent<TSelf, TNum> where TSelf : class, IMeasure<TSelf, TNum>, IDefaultRateComponent where TNum : struct
    {
        TSelf GetMeasure(string name, TNum quantity);
        TSelf GetMeasure(TNum quantity);
        TSelf GetMeasure(IMeasurement measurement, TNum quantity);
    }

    public interface IMeasure<TSelf, TNum, TEnum> : IMeasure<TSelf, TNum>, IMeasureUnit<TEnum> where TSelf : class, IMeasure<TSelf, TNum> where TNum : struct where TEnum : struct, Enum
    {
        TSelf GetMeasure(TEnum measureUnit, TNum quantity);
        TSelf GetMeasure(TSelf other);
        TSelf GetMeasure(TEnum measureUnit);
    }
}
