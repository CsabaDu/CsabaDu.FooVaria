﻿namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseRate : IBaseMeasure<IBaseRate, IMeasurable>, IQuantity<decimal>/*, IProportional<IBaseRate>*/, IDenominate
{
    MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();

    IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    IBaseRate GetBaseRate(IBaseMeasure numerator, IMeasurable denominatorMeasurement);
    IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasure denominator);
}
