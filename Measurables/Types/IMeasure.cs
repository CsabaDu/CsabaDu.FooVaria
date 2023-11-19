namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IMeasure : IRateComponent<IMeasure>, ILimitable, ICalculate, ICalculate<decimal, IMeasure>
    {
        //IMeasure GetMeasure(ValueType quantity, string name); //
        //IMeasure GetMeasure(ValueType quantity, Enum measureUnit); //
        //IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName); //
        //IMeasure GetMeasure(ValueType quantity, string customName, decimal exchangeRate);
        //IMeasure GetMeasure(ValueType quantity); //
        //IMeasure GetMeasure(ValueType quantity, IMeasurement measurement); //
        //IMeasure GetMeasure(IRateComponent baseMeasure); //
        //IMeasure GetMeasure(IMeasure other); //
    }

    public interface IMeasure<T, U> : IMeasure, IDefaultRateComponent<T, U> where T : class, IMeasure, IDefaultRateComponent where U : struct
    {
        T GetMeasure(string name, U quantity);
        T GetMeasure(U quantity);
        T GetMeasure(IMeasurement measurement, U quantity);
    }

    public interface IMeasure<T, U, W> : IMeasure<T, U>, IMeasureUnit<W> where T : class, IMeasure<T, U>, IDefaultRateComponent where U : struct where W : struct, Enum
    {
        T GetMeasure(W measureUnit, U quantity);
        T GetMeasure(T other);
        T GetMeasure(W measureUnit);
    }
}
