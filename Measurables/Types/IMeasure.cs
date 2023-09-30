﻿namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasure : IBaseMeasure, ILimitable, ICalculable, ICalculate<decimal, IMeasure>
{
    IMeasure GetMeasure(ValueType quantity, string name);
    IMeasure GetMeasure(ValueType quantity, Enum measureUnit);
    IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName);
    IMeasure GetMeasure(ValueType quantity, string customName, decimal exchangeRate);
    IMeasure GetMeasure(ValueType quantity);
    IMeasure GetMeasure(Enum measureUnit);
    IMeasure GetMeasure(ValueType quantity, IMeasurement measurement);
    IMeasure GetMeasure(IBaseMeasure baseMeasure);
    IMeasure GetMeasure(IMeasure other);

}
