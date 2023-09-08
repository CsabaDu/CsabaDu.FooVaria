﻿namespace CsabaDu.FooVaria.Measurables.Types;

public interface ILimitedRate : IRate, ILimiter<ILimitedRate, IMeasure>
{
    ILimit Limit { get; init; }

    ILimitedRate GetLimitedRate(IMeasure numerator, string name, decimal? quantity = null, ILimit? limit = null);
    ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null, ILimit? limit = null);
    ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null, ILimit? limit = null);
    ILimitedRate GetLimitedRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null, ILimit? limit = null);
    ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null, ILimit? limit = null);
    ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);
    ILimitedRate GetLimitedRate(IRate rate, ILimit? limit = null);
    ILimitedRate GetLimitedRate(ILimitedRate? other = null, ILimit? limit = null);
    ILimitedRateFactory GetLimitedRateFactory();
    ILimit GetOrCreateLimit(ILimit limit);
}
