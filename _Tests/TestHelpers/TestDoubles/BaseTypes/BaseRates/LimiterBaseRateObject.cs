﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.BaseRates;

public sealed class LimiterBaseRateObject(IRootObject rootObject, string paramName) : BaseRateChild(rootObject, paramName), ILimiter
{
    public static LimiterBaseRateObject GetLimiterBaseRateObject(LimitMode? limitMode, Enum measureUnit, decimal defaultQuantity, MeasureUnitCode denominatorCode, IBaseRateFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            LimitMode = limitMode,
            ReturnValues = GetReturnValues(measureUnit, defaultQuantity, factory),
            DenominatorCode = denominatorCode,
        };
    }

    public static LimiterBaseRateObject GetLimiterBaseRateObject(DataFields fields, IBaseRateFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            LimitMode = fields.limitMode,
            ReturnValues = GetReturnValues(fields.measureUnit, fields.defaultQuantity, factory),
            DenominatorCode = fields.denominatorCode,
        };
    }

    private LimitMode? LimitMode { get; set; }

    public override LimitMode? GetLimitMode() => LimitMode;
}
