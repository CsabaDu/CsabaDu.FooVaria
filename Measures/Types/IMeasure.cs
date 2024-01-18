namespace CsabaDu.FooVaria.Measures.Types
{
    public interface IMeasure : IBaseMeasure<IMeasure>, ILimitable<IMeasure>, ICalculate<IMeasure, decimal>, IFit<IBaseMeasure>
    {
        IMeasurement Measurement { get; init; }
    }

    public interface IMeasure<TSelf, TNum> : IMeasure, IDefaultBaseMeasure<TSelf, TNum>
        where TSelf : class, IMeasure, IDefaultBaseMeasure
        where TNum : struct
    {
        TSelf GetMeasure(string name, TNum quantity);
        TSelf GetMeasure(IMeasurement measurement, TNum quantity);
    }

    public interface IMeasure<TSelf, TNum, TEnum> : IMeasure<TSelf, TNum>, IMeasureUnit<TEnum>
        where TSelf : class, IMeasure, IDefaultBaseMeasure, IMeasureUnit
        where TNum : struct
        where TEnum : struct, Enum
    {
        TSelf GetMeasure(TEnum measureUnit, TNum quantity);
        TSelf GetMeasure(TEnum measureUnit);
    }
}
