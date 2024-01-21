﻿namespace CsabaDu.FooVaria.BaseMeasures.Behaviors;

public interface ILimitable<TSelf> : ILimitable
    where TSelf : class, IBaseMeasure, ILimitable
{
    TSelf ConvertToLimitable(ILimiter limiter);
}
