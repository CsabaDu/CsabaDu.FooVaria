﻿namespace CsabaDu.FooVaria.Measurables.Markers;

public interface IRateComponent<out T> where T : class, IMeasurable
{
    T? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
    T GetDefault(MeasureUnitTypeCode measureUnitTypeCode);
    RateComponentCode? GetRateComponentCode();
}
