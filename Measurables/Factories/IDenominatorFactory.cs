﻿namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IDenominatorFactory : IBaseMeasureFactory
{
    IDenominator Create(string name, ValueType quantity);
    IDenominator Create(Enum measureUnit, ValueType quantity);
    IDenominator Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity);
    IDenominator Create(IMeasurement measurement, ValueType quantity);
    IDenominator Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);
    IDenominator Create(IBaseMeasure baseMeasure, ValueType quantity);
    IDenominator Create(IDenominator denominator);
}

