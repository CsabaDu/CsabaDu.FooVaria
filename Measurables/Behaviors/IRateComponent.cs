﻿namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IRateComponent<out T> : IRateComponent where T : class, IMeasurable, IRateComponent
{
    T? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
    RateComponentCode GetRateComponentCode();
}
