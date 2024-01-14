﻿namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IExchangeRate
{
    decimal GetExchangeRate();
    decimal GetExchangeRate(Enum measureUnit);

    void ValidateExchangeRate(decimal exchangeRate, string paramName);
}
