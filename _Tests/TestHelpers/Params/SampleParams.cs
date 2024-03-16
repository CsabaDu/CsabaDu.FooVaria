﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.Params;

public static class SampleParams
{
    public const LimitMode DefaultLimitMode = default;

    public static readonly int MeasureUnitCodeCount = MeasureUnitCodes.Length;
    public static readonly MeasureUnitCode NotDefinedMeasureUnitCode = (MeasureUnitCode)MeasureUnitCodeCount;

    public static Enum GetNotDefinedMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        Type measureUnitType = measureUnitCode.GetMeasureUnitType();
        int count = Enum.GetNames(measureUnitType).Length;

        return (Enum)Enum.ToObject(measureUnitType, count);
    }

}
