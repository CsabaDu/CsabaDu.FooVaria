﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.Quantifiables;

public sealed class LimiterQuantifiableObject(IRootObject rootObject, string paramName) : QuantifiableChild(rootObject, paramName), ILimiter
{
    public static LimiterQuantifiableObject GetLimiterQuantifiableObject(LimitMode limitMode, Enum measureUnit, decimal defaultQuantity, IQuantifiableFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = GetReturn(measureUnit, defaultQuantity, factory),
            LimitMode = limitMode,
        };
    }

    private LimitMode LimitMode { get; set; }

    public LimitMode? GetLimitMode() => LimitMode;
}
