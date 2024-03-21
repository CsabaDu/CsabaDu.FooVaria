namespace CsabaDu.FooVaria.Measures.Types
{
    public interface IMeasure : IBaseMeasure<IMeasure>, ICalculate<IMeasure, decimal>
    {
        IMeasureFactory Factory { get; init; }
    }

    public interface IMeasure<TSelf, TNum> : IMeasure, IBaseMeasure<TSelf, TNum>
        where TSelf : class, IMeasure
        where TNum : struct
    {
        TSelf GetMeasure(string name, TNum quantity);
        TSelf GetMeasure(IMeasurement measurement, TNum quantity);
    }

    public interface IMeasure<TSelf, TNum, TEnum> : IMeasure<TSelf, TNum>, IMeasureUnit<TEnum>, IExchange<TSelf, TEnum>
        where TSelf : class, IMeasure
        where TNum : struct
        where TEnum : struct, Enum
    {
        IMeasurement Measurement { get; init; }

        TSelf GetMeasure(TEnum measureUnit, TNum quantity);
        TSelf GetMeasure(TEnum measureUnit);
    }
}
