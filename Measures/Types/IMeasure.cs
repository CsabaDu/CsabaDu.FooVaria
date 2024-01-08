﻿namespace CsabaDu.FooVaria.Measures.Types
{
    public interface IMeasure : IBaseMeasure, ILimitable, ICalculate<IMeasure, decimal>
    {
        IMeasure GetMeasure(IBaseMeasure rateComponent);
    }

    public interface IMeasure<TSelf, TNum> : IMeasure/*, IRateComponent<TSelf, TNum>*/
        where TSelf : class, IMeasure/*, IDefaultRateComponent*/
        where TNum : struct
    {
        TSelf GetMeasure(string name, TNum quantity);
        TSelf GetMeasure(IBaseMeasurement measurement, TNum quantity);
    }

    public interface IMeasure<TSelf, TNum, TEnum> : IMeasure<TSelf, TNum>, IMeasureUnit<TEnum>
        where TSelf : class, IMeasure/*, IDefaultRateComponent*/, IMeasureUnit
        where TNum : struct
        where TEnum : struct, Enum
    {
        TSelf GetMeasure(TEnum measureUnit, TNum quantity);
        TSelf GetMeasure(TEnum measureUnit);
    }
}
