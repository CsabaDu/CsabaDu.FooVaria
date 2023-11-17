namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IMeasure : IBaseMeasure, ILimitable, ICalculate, ICalculate<decimal, IMeasure>
    {
        IMeasure GetMeasure(ValueType quantity, string name); //
        IMeasure GetMeasure(ValueType quantity, Enum measureUnit); //
        IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName); //
        IMeasure GetMeasure(ValueType quantity, string customName, decimal exchangeRate);
        IMeasure GetMeasure(ValueType quantity); //
        IMeasure GetMeasure(ValueType quantity, IMeasurement measurement); //
        IMeasure GetMeasure(IBaseMeasure baseMeasure); //
        IMeasure GetMeasure(IMeasure other); //
    }

    public interface IMeasure<out T, U> : IMeasure, IDefaultBaseMeasure<T, U> where T : class, IMeasure, IDefaultBaseMeasure where U : struct
    {
        T GetMeasure(U quantity, string name);
        T GetMeasure(U quantity);
        T GetMeasure(U quantity, IMeasurement measurement);
    }

    public interface IMeasure<T, U, W> : IMeasure<T, U>, IMeasureUnit<W> where T : class, IMeasure<T, U>, IDefaultBaseMeasure where U : struct where W : struct, Enum
    {
        T GetMeasure(U quantity, W measureUnit);
        T GetMeasure(T other);
        T GetMeasure(W measureUnit);
    }
}
