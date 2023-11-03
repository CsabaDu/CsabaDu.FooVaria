namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IMeasure : IBaseMeasure, ILimitable, ICalculable, ICalculate<decimal, IMeasure>
    {
        IMeasure GetMeasure(ValueType quantity, string name);
        IMeasure GetMeasure(ValueType quantity, Enum measureUnit);
        IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName);
        IMeasure GetMeasure(ValueType quantity, string customName, decimal exchangeRate);
        IMeasure GetMeasure(ValueType quantity);
        //IMeasure GetMeasure(Enum measureUnit);
        IMeasure GetMeasure(ValueType quantity, IMeasurement measurement);
        IMeasure GetMeasure(IBaseMeasure baseMeasure);
        IMeasure GetMeasure(IMeasure other);

    }

    public interface IMeasure<T, U, W> : IMeasure, IDefaultRateComponent<T, U>, IMeasureUnit<W> where T : class, IMeasure, IDefaultRateComponent where U : struct where W : struct, Enum
    {
        T GetMeasure(U quantity, W measureUnit);
        T GetMeasure(U quantity, string name);
        T GetMeasure(U quantity);
        T GetMeasure(U quantity, IMeasurement measurement);
        T GetMeasure(T other);
        T GetMeasure(W measureUnit);
    }
}
