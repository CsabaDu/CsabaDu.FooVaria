﻿namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IMeasureUnit
{
    Enum GetMeasureUnit();
    bool IsDefinedMeasureUnit(Enum measureUnit);

    void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null);
}

